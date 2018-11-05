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
    /// 职位数据处理接口
    /// </summary>
    public interface IT_PositionRepository : IBaseRepository<T_Job>
    {
        /// <summary>
        /// 新增岗位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：重复，3：失败</returns>
        int Add(JobDto dto);

        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int Update(JobDto dto);

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="dto">dto</param>
        /// <param name="usedJobName">已经使用的岗位集合</param>
        /// <returns></returns>
        bool Delete(JobDeleteDto dto, out List<string> usedJobName);

        /// <summary>
        /// 获得岗位列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        List<JobDto> GetJobList(JobSearchDto param, out int reCount);

        /// <summary>
        /// 获得当前系统所有岗位
        /// </summary>
        /// <returns></returns>
        List<JobDto> GetJobList();
    }
}
