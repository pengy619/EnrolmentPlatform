using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IBLL.Systems
{
    public interface IT_LogSettingService : IBaseService<T_LogSetting>
    {
        /// <summary>
        /// 根据Key查询操作日志
        /// </summary>
        /// <param name="param">DTO</param>
        /// <returns></returns>
        IList<LogSettingDTO> FindLogSettingByKey(LogSettingDTO param, out int records);
        /// <summary>
        /// 根据企业查询操作日志
        /// </summary>
        /// <param name="param">DTO</param>
        /// <returns></returns>
        IList<LogSettingDTO> GetLogSettingByEnterpriseId(LogSettingDTO param, out int records);
        IList<LogSettingDTO> GetLogSetting_Scenic(LogSettingDTO param, out int records);
    }
}
