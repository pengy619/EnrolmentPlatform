using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Systems
{
    /// <summary>
    /// 角色数据处理
    /// </summary>
    public class T_RoleRepository : BaseRepository<T_Role>, IT_RoleRepository
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dto">角色DTO</param>
        /// <returns>1：成功，2：已经存在，3：失败</returns>
        public int AddRole(RoleDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();

            int classify = (int)dto.SystemType;
            //检查是否重复名称
            if (dbContext.T_Role.Count(a => a.RoleName == dto.RoleName
                    && a.Classify == classify
                    && a.EnterpriseId == dto.EnterpriseId) > 0)
            {
                return 2;
            }

            //添加角色基本信息
            T_Role role = new T_Role()
            {
                Id = Guid.NewGuid(),
                Classify = classify,
                EnterpriseId = dto.EnterpriseId,
                RoleName = dto.RoleName,
                Status = dto.IntStatus,
                CreatorAccount = dto.CurUserAccount,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.CurUserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.CurUserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            };
            dbContext.T_Role.Add(role);

            //添加角色权限
            if (dto.PermissionList != null && dto.PermissionList.Count > 0)
            {
                foreach (var item in dto.PermissionList)
                {
                    dbContext.T_RolePermissionsRelation.Add(new T_RolePermissionsRelation()
                    {
                        Id = Guid.NewGuid(),
                        RoleId = role.Id,
                        PermissionsId = item
                    });
                }
            }

            //保存并记录日志
            dbContext.ModuleKey = role.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "角色添加";
            return (dbContext.SaveChanges() > 0) ? 1 : 3;
        }

        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public RoleDto GetRole(Guid roleId)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            T_Role role = dbContext.T_Role.FirstOrDefault(a => a.Id == roleId);
            RoleDto dto = new RoleDto();
            dto.EnterpriseId = role.EnterpriseId;
            dto.IntStatus = role.Status;
            dto.RoleId = role.Id;
            dto.RoleName = role.RoleName;
            dto.SystemType = (SystemTypeEnum)role.Classify;
            dto.PermissionList = this.GetDbContext().T_RolePermissionsRelation.Where(a => a.RoleId == roleId)
                .Select(a => a.PermissionsId).ToList();
            return dto;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="usedRoleName">已经使用的角色集合</param>
        /// <returns></returns>
        public bool DeleteRole(Guid[] roleIds, out List<string> usedRoleName)
        {
            var dbContext = this.GetDbContext();
            //是否存在已被用户使用的角色
            usedRoleName = (from a in dbContext.T_AccountBasic
                            join b in dbContext.T_Role on a.RoleId equals b.Id
                            where roleIds.Contains(a.RoleId)
                            select b.RoleName).Distinct().ToList();
            if (usedRoleName != null && usedRoleName.Count > 0)
            {
                return false;
            }

            //删除
            var roleList = dbContext.T_Role.Where(a => roleIds.Contains(a.Id)).ToList();
            foreach (var item in roleList)
            {
                //先删除角色权限关联
                List<T_RolePermissionsRelation> realationList = dbContext.T_RolePermissionsRelation
                    .Where(a => a.RoleId == item.Id).ToList();
                foreach (var realation in realationList)
                {
                    dbContext.T_RolePermissionsRelation.Remove(realation);
                }

                //删除角色
                dbContext.T_Role.Remove(item);
            }

            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto">角色DTO</param>
        /// <returns>1：成功，2：已经存在，3：失败</returns>
        public int UpdateRole(RoleDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            //添加角色基本信息
            T_Role role = dbContext.T_Role.FirstOrDefault(a => a.Id == dto.RoleId);

            //检查是否重复名称
            if (dbContext.T_Role.Count(a => a.RoleName == dto.RoleName && a.Id != dto.RoleId
                    && a.Classify == role.Classify
                    && a.EnterpriseId == role.EnterpriseId) > 0)
            {
                return 2;
            }

            if (role == null) return 2;
            role.RoleName = dto.RoleName;
            role.Status = dto.IntStatus;
            role.LastModifyTime = DateTime.Now;
            role.LastModifyUserId = dto.CurUserId;
            dbContext.Entry(role).State = EntityState.Modified;

            //先删除已有权限
            List<T_RolePermissionsRelation> realationList = dbContext.T_RolePermissionsRelation
                .Where(a => a.RoleId == dto.RoleId).ToList();
            foreach (var realation in realationList)
            {
                dbContext.T_RolePermissionsRelation.Remove(realation);
            }

            //添加角色权限
            if (dto.PermissionList != null && dto.PermissionList.Count > 0)
            {
                foreach (var item in dto.PermissionList)
                {
                    dbContext.T_RolePermissionsRelation.Add(new T_RolePermissionsRelation()
                    {
                        Id = Guid.NewGuid(),
                        RoleId = role.Id,
                        PermissionsId = item
                    });
                }
            }

            //保存并记录日志
            dbContext.ModuleKey = role.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "角色修改";
            return (dbContext.SaveChanges()) > 0 ? 1 : 3;
        }

        /// <summary>
        /// 获得角色列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<RoleDto> GetRoleList(RoleSearchDto param, out int reCount)
        {
            var noStatus = param.Status.HasValue == false;
            var noName = string.IsNullOrWhiteSpace(param.RoleName);
            var _lst = this.LoadPageEntitiesOrderByField(
                (a => a.IsDelete == false && a.Classify == (int)param.SystemType && a.EnterpriseId == param.EnterpriseId
                  && (noStatus || a.Status == (int)param.Status.Value)
                  && (noName || a.RoleName.Contains(param.RoleName))),
                param.Field ?? "Unix",
                param.Limit,
                param.Page,
                out reCount,
                (param.Sort ?? "desc").ToLower().Equals("asc")
                ).ToList();
            return _lst.Select(a => new RoleDto()
            {
                RoleId = a.Id,
                IntStatus = a.Status,
                RoleName = a.RoleName
            }).ToList();
        }

        /// <summary>
        /// 获得当前系统所有正常的角色
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<RoleDto> GetRoleList(RoleSearchDto param)
        {
            return this.LoadEntities((a => a.IsDelete == false && a.Classify == (int)param.SystemType
                        && a.EnterpriseId == param.EnterpriseId && a.Status == (int)StatusBaseEnum.Enabled))
                .Select(a => new RoleDto()
                {
                    IntStatus = a.Status,
                    RoleId = a.Id,
                    RoleName = a.RoleName
                }).ToList();
        }

        /// <summary>
        /// 获得系统权限
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <returns></returns>
        public List<ModuleDto> GetRolePermissions(SystemTypeEnum type)
        {
            List<T_Permissions> allList = this.GetDbContext().T_Permissions.Where(a => a.Classify == (int)type)
                .OrderBy(a => a.Sort).ToList();
            //模块
            List<ModuleDto> list = allList.Where(a => a.ParentId == Guid.Empty).Select(a => new ModuleDto
            {
                ModuleId = a.Id,
                ModuleName = a.Name
            }).ToList();

            //菜单
            foreach (var item in list)
            {
                item.MenuList = allList.Where(a => a.ParentId == item.ModuleId).Select(a => new MenuDto()
                {
                    MenuId = a.Id,
                    MenuName = a.Name
                }).ToList();
            }

            return list;
        }

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="roleId">权限ID</param>
        /// <returns></returns>
        public List<RolePermissionDto> GetRolePermissionList(Guid roleId, SystemTypeEnum systemTypeEnum)
        {
            int classify = (int)systemTypeEnum;
            var dbContext = this.GetDbContext();
            var list = from a in dbContext.T_RolePermissionsRelation
                       join b in dbContext.T_Permissions on a.PermissionsId equals b.Id
                       where b.Classify == classify && a.RoleId == roleId
                       select new RolePermissionDto()
                       {
                           Id = b.Id,
                           Action = b.Action,
                           Area = b.Area,
                           Controller = b.Controller,
                           Icon = b.Icon,
                           Level = b.Level,
                           Name = b.Name,
                           ParentId = b.ParentId,
                           Sort = b.Sort
                       };

            //用户授权的权限
            List<RolePermissionDto> dtoList = list.OrderBy(a => a.Sort).ToList();

            //如果是景区后台则需要包含已存在的下级权限
            if (dtoList != null && dtoList.Count > 0)
            {
                var idList = dtoList.Where(a => a.Level == 3).Select(a => a.Id).ToList();
                var actionList = dbContext.T_Permissions.Where(a => idList.Contains(a.ParentId) && idList.Contains(a.Id) == false)
                   .Select(b => new RolePermissionDto()
                   {
                       Id = b.Id,
                       Action = b.Action,
                       Area = b.Area,
                       Controller = b.Controller,
                       Icon = b.Icon,
                       Level = b.Level,
                       Name = b.Name,
                       ParentId = b.ParentId,
                       Sort = b.Sort
                   }).OrderBy(a => a.Sort).ToList();

                //操作权限不为空
                if (actionList != null && actionList.Count > 0)
                {
                    //一并返回
                    dtoList.AddRange(actionList);
                }
            }

            return dtoList;
        }

        /// <summary>
        /// 获得系统所有完整权限
        /// </summary>
        /// <param name="systemTypeEnum">系统类型</param>
        /// <returns></returns>
        public List<RolePermissionDto> GetAllPermissionList(SystemTypeEnum systemTypeEnum)
        {
            int classify = (int)systemTypeEnum;
            var dbContext = this.GetDbContext();
            var list = from b in dbContext.T_Permissions
                       where b.Classify == classify
                       select new RolePermissionDto()
                       {
                           Id = b.Id,
                           Action = b.Action,
                           Area = b.Area,
                           Controller = b.Controller,
                           Icon = b.Icon,
                           Level = b.Level,
                           Name = b.Name,
                           ParentId = b.ParentId,
                           Sort = b.Sort
                       };
            return list.OrderBy(a => a.Sort).ToList();
        }

        /// <summary>
        /// 根据ID获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_Permissions GetPermissionById(Guid id)
        {
            var dbContext = this.GetDbContext();
            var query = from p in dbContext.T_Permissions
                        where p.Id == id
                        select p;
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 新增修改权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMsg UpdatePermission(T_Permissions entity)
        {
            ResultMsg msg = new ResultMsg();
            var dbContext = this.GetDbContext();
            //新增
            if (entity.Id.Equals(Guid.Empty))
            {
                entity.Id = Guid.NewGuid();
                dbContext.T_Permissions.Add(entity);
            }
            //修改
            else
            {
                var permission = dbContext.T_Permissions.Find(entity.Id);
                permission.Action = entity.Action ?? "";
                permission.Area = entity.Area??"";
                permission.Controller = entity.Controller ?? "";
                permission.Icon = entity.Icon;
                permission.Level = entity.Level;
                permission.Name = entity.Name;
                permission.Sort = entity.Sort;
                dbContext.Entry(permission).State = EntityState.Modified;
            }
            msg.IsSuccess = dbContext.SaveChanges() > 0;
            msg.Data = entity.Id;
            return msg;
        }
    }
}
