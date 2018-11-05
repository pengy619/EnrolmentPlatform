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
    public class T_SystemLoginLogService : BaseService<T_SystemLoginLog>, IT_SystemLoginLogService, IInterceptorLogic
    {
        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_SystemLoginLogRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }
        /// <summary>
        /// 得到日志
        /// </summary>
        /// <param name="param"></param>
        /// <param name="records"></param>
        /// <returns></returns>
       
        public IList<T_SystemLoginLog> GetLoginLog(LoginLogDto param, out int records)
        {
            var _whereLambda = ExtLinq.True<T_SystemLoginLog>();
            if (!param.KeyWrod.IsEmpty())
            {
                _whereLambda = _whereLambda.And(it => it.Account.Contains(param.KeyWrod));

            }
            _whereLambda = _whereLambda.And(it => it.CreatorTime >= param.StartDate && it.CreatorTime <= param.EndDate);
            var _lst = CurrentRepository.LoadPageEntitiesOrderByField(
                _whereLambda,
                "Unix",
               param.Limit,
               param.Page,
               out records,
               (param.Sort ?? "desc").ToLower().Equals("asc")
                ).ToList();
            return _lst;
        }
        /// <summary>
        /// 得到企业登录日志
        /// </summary>
        /// <param name="param"></param>
        /// <param name="records"></param>
        /// <returns></returns>
       
        public IList<T_SystemLoginLog> GetEnterpriseLoginLog(LoginLogDto param, out int records)
        {
            return (this.CurrentRepository as IT_SystemLoginLogRepository).GetEnterpriseLoginLog(param, out records);
        }
    }
}
