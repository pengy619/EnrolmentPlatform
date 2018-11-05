using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    public class T_LogSettingService : BaseService<T_LogSetting>, IT_LogSettingService, IInterceptorLogic
    {
        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_LogSettingRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }
        /// <summary>
        /// 得到操作日志根据主键或模块编号
        /// </summary>
        /// <param name="param"></param>
        /// <param name="records"></param>
        /// <returns></returns>
       
        public IList<LogSettingDTO> FindLogSettingByKey(LogSettingDTO param, out int records)
        {

            return (this.CurrentRepository as IT_LogSettingRepository).GetLogSettingByKey(param, out records);
        }
        /// <summary>
        /// 查询企业下操作日志
        /// </summary>
        /// <param name="param"></param>
        /// <param name="records"></param>
        /// <returns></returns>
       
        public IList<LogSettingDTO> GetLogSettingByEnterpriseId(LogSettingDTO param, out int records)
        {

            //根据创建者Id
            return (this.CurrentRepository as IT_LogSettingRepository).GetLogSettingByEnterpriseId(param, out records);
        }
        /// <summary>
        /// 景区查看日志
        /// </summary>
        /// <param name="param"></param>
        /// <param name="records"></param>
        /// <returns></returns>
       
        public IList<LogSettingDTO> GetLogSetting_Scenic(LogSettingDTO param, out int records)
        {
            return (this.CurrentRepository as IT_LogSettingRepository).GetLogSetting_Scenic(param, out records);
        }
    }
}
