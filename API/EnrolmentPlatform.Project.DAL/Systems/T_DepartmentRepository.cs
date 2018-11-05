using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Infrastructure;
using System.Data.Entity;

namespace EnrolmentPlatform.Project.DAL.Systems
{
    /// <summary>
    /// 部门数据处理
    /// </summary>
    public class T_DepartmentRepository : BaseRepository<T_Department>, IT_DepartmentRepository
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：重复，3：失败</returns>
        public int Add(DepartmentDto dto)
        {
            //检查是否重复名称
            var _dpartment = base.LoadEntities(a => a.DepartmentName == dto.DepartmentName).FirstOrDefault();
            if (_dpartment != null)
            {
                return 2;
            }
            //添加部门基本信息
            T_Department department = new T_Department()
            {
                Id = Guid.NewGuid(),
                DepartmentName = dto.DepartmentName,
                Sort = 0,
                CreatorAccount = dto.CreatorAccount,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.CreateUserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.CreateUserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            };


            return base.AddEntity(department, E_DbClassify.Write, "部门添加") > 0 ? 1 : 3;
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int Update(DepartmentDto dto)
        {
            //检查是否重复名称
            if (base.LoadEntities(a => a.DepartmentName == dto.DepartmentName && a.Id != dto.DepartmentId.Value).Count() > 0)
            {
                return 2;
            }
            //添加部门基本信息
            T_Department department = base.FindEntityById(dto.DepartmentId.Value);
            if (department == null) return 2;
            department.DepartmentName = dto.DepartmentName;
            department.LastModifyTime = DateTime.Now;
            department.LastModifyUserId = dto.CreateUserId;

            return base.UpdateEntity(department, E_DbClassify.Write, "部门修改") > 0 ? 1 : 3;

            
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deleteDto">deleteDto</param>
        /// <param name="usedDepartmentName">已经使用的部门集合</param>
        /// <returns></returns>
        public bool Delete(DepartmentDeleteDto deleteDto, out List<string> usedDepartmentName)
        {
            var dbContext = this.GetDbContext();
            //是否存在已被用户使用的部门
            usedDepartmentName = (from a in dbContext.T_AccountBasic
                                  join b in dbContext.T_Department on a.DepartmentId equals b.Id
                                  where deleteDto.DepartmentIds.Contains(a.DepartmentId)
                                  select b.DepartmentName).Distinct().ToList();
            if (usedDepartmentName != null && usedDepartmentName.Count > 0)
            {
                return false;
            }

            //删除
            var departmentList = dbContext.T_Department.Where(a => deleteDto.DepartmentIds.Contains(a.Id)).ToList();
            dbContext.T_Department.RemoveRange(departmentList);
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<DepartmentDto> GetDepartmentList(DepartmentSearchDto param, out int reCount)
        {
            var _lst = this.LoadPageEntitiesOrderByField(
                (a => a.IsDelete == false),
                param.Field ?? "Unix",
                param.Limit,
                param.Page,
                out reCount,
                (param.Sort ?? "desc").ToLower().Equals("asc")
                ).ToList();
            return _lst.Select(a => new DepartmentDto()
            {
                DepartmentId = a.Id,
                DepartmentName = a.DepartmentName
            }).ToList();
        }

        /// <summary>
        /// 获得当前系统所有部门
        /// </summary>
        /// <returns></returns>
        public List<DepartmentDto> GetDepartmentList()
        {
            return this.LoadEntities(a => a.IsDelete == false)
                .Select(a => new DepartmentDto()
                {
                    DepartmentId = a.Id,
                    DepartmentName = a.DepartmentName
                }).ToList();
        }
    }
}
