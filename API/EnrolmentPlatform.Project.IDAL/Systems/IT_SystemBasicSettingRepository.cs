using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums.Systems;

namespace EnrolmentPlatform.Project.IDAL.Systems
{
    public interface IT_SystemBasicSettingRepository : IBaseRepository<T_SystemBasicSetting>
    {
        T_SystemBasicSetting GetSystemBasicSettingByKey(SystemBasicSettingEnum systemBasicSettingEnum);
    }
}
