using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IDAL.Systems
{
    public interface IT_LogSettingRepository : IBaseRepository<T_LogSetting>
    {
        /// <summary>
        /// 根据企业Id得到日志
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        IList<LogSettingDTO> GetLogSettingByEnterpriseId(LogSettingDTO param, out int records);

        IList<LogSettingDTO> GetLogSettingByKey(LogSettingDTO param, out int records);
        IList<LogSettingDTO> GetLogSetting_Scenic(LogSettingDTO param, out int records);
    }
}
