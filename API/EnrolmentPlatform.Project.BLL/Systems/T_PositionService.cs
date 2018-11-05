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
    /// 岗位服务层
    /// </summary>
    public class T_PositionService : BaseService<T_Job>, IT_PositionService, IInterceptorLogic
    {
        private IT_PositionRepository repository = null;

        public T_PositionService()
        {
            this.repository = DIContainer.Resolve<IT_PositionRepository>();
        }

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = repository;
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }

        /// <summary>
        /// 新增岗位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：重复，3：失败</returns>
        public int Add(JobDto dto)
        {
            return this.repository.Add(dto);
        }

        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int Update(JobDto dto)
        {
            return this.repository.Update(dto);
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="dto">dto</param>
        /// <param name="usedJobName">已经使用的岗位集合</param>
        /// <returns></returns>
        public bool Delete(JobDeleteDto dto, out List<string> usedJobName)
        {
            return this.repository.Delete(dto, out usedJobName);
        }

        /// <summary>
        /// 获得岗位列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
       
        public List<JobDto> GetJobList(JobSearchDto param, out int reCount)
        {
            return this.repository.GetJobList(param, out reCount);
        }

        /// <summary>
        /// 获得当前系统所有岗位
        /// </summary>
        /// <returns></returns>
       
        public List<JobDto> GetJobList()
        {
            return this.repository.GetJobList();
        }
    }
}
