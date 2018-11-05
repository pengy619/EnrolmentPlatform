using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;

namespace EnrolmentPlatform.Project.DAL.Systems
{
    public class T_SystemBasicSettingRepository : BaseRepository<T_SystemBasicSetting>, IT_SystemBasicSettingRepository
    {

        public T_SystemBasicSetting GetSystemBasicSettingByKey(SystemBasicSettingEnum systemBasicSettingEnum)
        {
            int key = (int)systemBasicSettingEnum;
            T_SystemBasicSetting systemBasicSetting = base.LoadEntities(it => it.Key == key).FirstOrDefault();
            return systemBasicSetting;
        }
    }
}
