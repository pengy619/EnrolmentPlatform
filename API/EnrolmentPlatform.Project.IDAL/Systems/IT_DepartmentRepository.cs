using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IDAL.Systems
{
    /// <summary>
    /// 部门数据处理接口
    /// </summary>
    public interface IT_DepartmentRepository : IBaseRepository<T_Department>
    {
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：重复，3：失败</returns>
        int Add(DepartmentDto dto);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int Update(DepartmentDto dto);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deleteDto">deleteDto</param>
        /// <param name="usedDepartmentName">已经使用的部门集合</param>
        /// <returns></returns>
        bool Delete(DepartmentDeleteDto deleteDto, out List<string> usedDepartmentName);

        /// <summary>
        /// 获得部门列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        List<DepartmentDto> GetDepartmentList(DepartmentSearchDto param, out int reCount);

        /// <summary>
        /// 获得当前系统所有部门
        /// </summary>
        /// <returns></returns>
        List<DepartmentDto> GetDepartmentList();
    }
}
