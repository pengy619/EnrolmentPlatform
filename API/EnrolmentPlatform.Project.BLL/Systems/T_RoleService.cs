using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class T_RoleService : BaseService<T_Role>, IT_RoleService, IInterceptorLogic
    {
        private IT_RoleRepository repository = null;

        public T_RoleService()
        {
            this.repository = DIContainer.Resolve<IT_RoleRepository>();
        }

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = repository;
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dto">角色DTO</param>
        /// <returns>1：成功，2：已经存在</returns>
        public int AddRole(RoleDto dto)
        {
            return this.repository.AddRole(dto);
        }

        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public RoleDto GetRole(Guid roleId)
        {
            return this.repository.GetRole(roleId);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="msg">失败的时候消息</param>
        /// <returns></returns>
        public bool DeleteRole(Guid[] roleIds, out string msg)
        {
            //移除缓存
            foreach (var item in roleIds)
            {
                //移除缓存
                this.ClearRolePremission(item);
            }

            List<string> usedRole = null;
            bool ret = this.repository.DeleteRole(roleIds, out usedRole);
            msg = "";
            if (ret == false)
            {
                msg = "[" + string.Join(",", usedRole.ToArray()) + "]已经被使用,不能删除。";
            }
            return ret;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto">角色DTO</param>
        /// <returns>1：成功，2：已经存在</returns>
        public int UpdateRole(RoleDto dto)
        {
            //移除缓存
            this.ClearRolePremission(dto.RoleId.Value);
            //修改角色
            return this.repository.UpdateRole(dto);
        }

        /// <summary>
        /// 获得角色列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
       
        public List<RoleDto> GetRoleList(RoleSearchDto param, out int reCount)
        {
            return this.repository.GetRoleList(param, out reCount);
        }

        /// <summary>
        /// 获得当前系统所有正常的角色
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
       
        public List<RoleDto> GetRoleList(RoleSearchDto param)
        {
            return this.repository.GetRoleList(param);
        }

        /// <summary>
        /// 获得系统权限
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <returns></returns>
        public List<ModuleDto> GetRolePermissions(SystemTypeEnum type)
        {
            return this.repository.GetRolePermissions(type);
        }

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="roleId">权限ID</param>
        /// <returns></returns>
        public List<RolePermissionDto> GetRolePermissionList(Guid roleId, SystemTypeEnum systemTypeEnum)
        {
            // 缓存是否有值
            string redisKey = "RolePermission" + roleId.ToString();
            var obj = RedisHelper.Get(redisKey);
            if (obj != null && obj.ToString() != "")
            {
                // 如果有直接返回
                return (List<RolePermissionDto>)obj;
            }

            // 否则从数据库取出
            List<RolePermissionDto> list = this.repository.GetRolePermissionList(roleId, systemTypeEnum);
            if (list != null && list.Count > 0)
            {
                RedisHelper.Set(redisKey, list);
            }
            return list;
        }

        /// <summary>
        /// 清除权限
        /// </summary>
        /// <param name="roleId"></param>
        public bool ClearRolePremission(Guid roleId)
        {
            bool result = false;
            // 缓存是否有值
            string redisKeyToRole = "RolePermission" + roleId.ToString();
            result = RedisHelper.Remove(redisKeyToRole);
            return result;
        }
        /// <summary>
        /// 清除权限
        /// </summary>
        /// <param name="roleId"></param>
        public bool ClearRoleAndSystemPremission(Guid roleId, SystemTypeEnum systemTypeEnum)
        {
            bool result = false;
            // 缓存是否有值
            string redisKeyToRole = "RolePermission" + roleId.ToString();
            string redisKeyToSystem = "SystemPermission" + (int)systemTypeEnum;
            result = RedisHelper.Remove(redisKeyToSystem);
            if (result)
            {
                result = RedisHelper.Remove(redisKeyToRole);
            }
            return result;
        }
        /// <summary>
        /// 根据parentId得到权限列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private void GetPermissionsByParentId(List<RolePermissionDto> lst, Guid parentId, Guid roleId, SystemTypeEnum systemTypeEnum)
        {
            RolePermissionDto permission = (from it in GetAllPermissionList(systemTypeEnum)
                                            where it.Id == parentId
                                            select new RolePermissionDto
                                            {
                                                Id = it.Id,
                                                Action = it.Action,
                                                Area = it.Area,
                                                Controller = it.Controller,
                                                Icon = it.Icon,
                                                Level = it.Level,
                                                Name = it.Name,
                                                ParentId = it.ParentId,
                                                Sort = it.Sort
                                            }).FirstOrDefault();
            if (permission != null)
            {
                lst.Add(permission);
                GetPermissionsByParentId(lst, permission.ParentId, roleId, systemTypeEnum);
            }
        }
        /// <summary>
        /// 根据路经得到当前权限
        /// </summary>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>

        public List<RolePermissionDto> GetPermissionsByLocation(string area, string controller, Guid roleId, SystemTypeEnum systemTypeEnum, string action = "index")
        {
            List<RolePermissionDto> lst = new List<RolePermissionDto>();
            var permissionList = GetAllPermissionList(systemTypeEnum);
            var model = (from it in permissionList
                         where (string.IsNullOrEmpty(area) ? true : it.Area.ToLower().Equals(area.ToLower()))
                         && it.Controller.ToLower().Equals(controller.ToLower())
                         && it.Action.ToLower().Equals(action.ToLower())
                         select new RolePermissionDto
                         {
                             Id = it.Id,
                             Action = it.Action,
                             Area = it.Area,
                             Controller = it.Controller,
                             Icon = it.Icon,
                             Level = it.Level,
                             Name = it.Name,
                             ParentId = it.ParentId,
                             Sort = it.Sort
                         }).FirstOrDefault();

            if (model != null && model.Level != 4)//4级菜单取Title,修改增加方法也在一起无法区分
            {
                GetPermissionsByParentId(lst, model.ParentId, roleId, systemTypeEnum);
                lst.Add(model);
            }
            else
            {

                model = (from it in permissionList
                         where (string.IsNullOrEmpty(area) ? true : it.Area.ToLower().Equals(area.ToLower()))
                         && it.Controller.ToLower().Equals(controller.ToLower())
                         select new RolePermissionDto
                         {
                             Id = it.Id,
                             Action = it.Action,
                             Area = it.Area,
                             Controller = it.Controller,
                             Icon = it.Icon,
                             Level = it.Level,
                             Name = it.Name,
                             ParentId = it.ParentId,
                             Sort = it.Sort
                         }).OrderBy(it => it.Level).FirstOrDefault();
                lst.Add(new RolePermissionDto { Level = 100 }); //三级详情页
                if (model != null)
                {
                    GetPermissionsByParentId(lst, model.ParentId, roleId, systemTypeEnum);
                    lst.Add(model);
                }
            }
            return lst;
        }

        /// <summary>
        /// 获得系统所有完整权限
        /// </summary>
        /// <param name="systemTypeEnum">系统类型</param>
        /// <returns></returns>
        public List<RolePermissionDto> GetAllPermissionList(SystemTypeEnum systemTypeEnum)
        {
            // 缓存是否有值
            string redisKey = "SystemPermission" + (int)systemTypeEnum;
            var obj = RedisHelper.Get(redisKey);
            if (obj != null && obj.ToString() != "")
            {
                // 如果有直接返回
                return (List<RolePermissionDto>)obj;
            }

            // 否则从数据库取出
            List<RolePermissionDto> list = this.repository.GetAllPermissionList(systemTypeEnum);
            if (list != null && list.Count > 0)
            {
                RedisHelper.Set(redisKey, list);
            }
            return list;
        }


        /// <summary>
        /// 根据ID删除权限及下面所有子权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePermissionById(Guid id)
        {
            T_Permissions permissions = GetPermissionById(id);
            SystemTypeEnum classify = (SystemTypeEnum)permissions.Classify;
            //清除系统所有角色的权限缓存
            var list = this.GetRoleList(new RoleSearchDto() { SystemType = classify });
            foreach (var item in list)
            {
                this.ClearRolePremission(item.RoleId.Value);
            }

            //清除系统所有权限的缓存
            string redisKey = "SystemPermission" + permissions.Classify;
            RedisHelper.Remove(redisKey);
            string strSql = "EXEC SP_DeletePermissions @Id";
            SqlParameter[] Paras = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            int n = this.repository.ExecSql(strSql, E_DbClassify.Write, Paras);
            return n > 0 ? true : false;
        }

        /// <summary>
        /// 根据ID获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_Permissions GetPermissionById(Guid id)
        {
            return this.repository.GetPermissionById(id);
        }

        /// <summary>
        /// 新增 修改权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMsg UpdatePermission(T_Permissions entity)
        {
            //清除系统所有角色的权限缓存
            var list = this.GetRoleList(new RoleSearchDto() { SystemType = (SystemTypeEnum)entity.Classify });
            foreach (var item in list)
            {
                this.ClearRolePremission(item.RoleId.Value);
            }
            //清除系统所有权限的缓存
            string redisKey = "SystemPermission" + entity.Classify;
            RedisHelper.Remove(redisKey);
            return this.repository.UpdatePermission(entity);
        }
    }
}
