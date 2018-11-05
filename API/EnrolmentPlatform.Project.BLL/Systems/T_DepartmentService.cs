using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    /// <summary>
    /// 部门服务层
    /// </summary>
    public class T_DepartmentService : BaseService<T_Department>, IT_DepartmentService, IInterceptorLogic
    {
        private IT_DepartmentRepository repository = null;

        public T_DepartmentService()
        {
            this.repository = DIContainer.Resolve<IT_DepartmentRepository>();
        }

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = repository;
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：重复，3：失败</returns>
        public int Add(DepartmentDto dto)
        {
            return this.repository.Add(dto);
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int Update(DepartmentDto dto)
        {
            return this.repository.Update(dto);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deleteDto">deleteDto</param>
        /// <param name="usedDepartmentName">已经使用的部门集合</param>
        /// <returns></returns>
        public bool Delete(DepartmentDeleteDto deleteDto, out List<string> usedDepartmentName)
        {
            return this.repository.Delete(deleteDto, out usedDepartmentName);
        }

        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
       
        public List<DepartmentDto> GetDepartmentList(DepartmentSearchDto param, out int reCount)
        {
            return this.repository.GetDepartmentList(param, out reCount);
        }

        /// <summary>
        /// 获得当前系统所有部门
        /// </summary>
        /// <returns></returns>
       
        public List<DepartmentDto> GetDepartmentList()
        {
            return this.repository.GetDepartmentList();
        }
    }
}
