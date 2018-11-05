using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Systems
{
    /// <summary>
    /// 岗位数据处理
    /// </summary>
    public class T_PositionRepository : BaseRepository<T_Job>, IT_PositionRepository
    {
        /// <summary>
        /// 新增岗位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：重复，3：失败</returns>
        public int Add(JobDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();

            //检查是否重复名称
            if (dbContext.T_Job.Count(a => a.JobName == dto.JobName) > 0)
            {
                return 2;
            }

            //添加岗位基本信息
            T_Job job = new T_Job()
            {
                Id = Guid.NewGuid(),
                JobName = dto.JobName,
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
            dbContext.T_Job.Add(job);

            //保存并记录日志
            dbContext.ModuleKey = job.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "岗位添加";
            return (dbContext.SaveChanges() > 0) ? 1 : 3;
        }

        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int Update(JobDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            //添加岗位基本信息
            T_Job job = dbContext.T_Job.FirstOrDefault(a => a.Id == dto.JobId.Value);

            //检查是否重复名称
            if (dbContext.T_Job.Count(a => a.JobName == dto.JobName && a.Id != dto.JobId.Value) > 0)
            {
                return 2;
            }

            if (job == null) return 2;
            job.JobName = dto.JobName;
            job.LastModifyTime = DateTime.Now;
            job.LastModifyUserId = dto.CreateUserId;
            dbContext.Entry(job).State = EntityState.Modified;

            //保存并记录日志
            dbContext.ModuleKey = job.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "岗位修改";
            return (dbContext.SaveChanges()) > 0 ? 1 : 3;
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="dto">dto</param>
        /// <param name="usedJobName">已经使用的岗位集合</param>
        /// <returns></returns>
        public bool Delete(JobDeleteDto dto, out List<string> usedJobName)
        {
            var dbContext = this.GetDbContext();
            //是否存在已被用户使用的岗位
            usedJobName = (from a in dbContext.T_AccountBasic
                                  join b in dbContext.T_Job on a.JobID equals b.Id
                                  where dto.JobIds.Contains(a.DepartmentId)
                                  select b.JobName).Distinct().ToList();
            if (usedJobName != null && usedJobName.Count > 0)
            {
                return false;
            }

            //删除
            var jobList = dbContext.T_Job.Where(a => dto.JobIds.Contains(a.Id)).ToList();
            foreach (var item in jobList)
            {
                //删除岗位
                dbContext.T_Job.Remove(item);
            }

            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 获得岗位列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<JobDto> GetJobList(JobSearchDto param, out int reCount)
        {
            var _lst = this.LoadPageEntitiesOrderByField(
                (a => a.IsDelete == false),
                param.Field ?? "Unix",
                param.Limit,
                param.Page,
                out reCount,
                (param.Sort ?? "desc").ToLower().Equals("asc")
                ).ToList();
            return _lst.Select(a => new JobDto()
            {
                JobId = a.Id,
                JobName = a.JobName
            }).ToList();
        }

        /// <summary>
        /// 获得当前系统所有岗位
        /// </summary>
        /// <returns></returns>
        public List<JobDto> GetJobList()
        {
            return this.LoadEntities(a => a.IsDelete == false)
                .Select(a => new JobDto()
                {
                    JobId = a.Id,
                    JobName = a.JobName
                }).ToList();
        }
    }
}
