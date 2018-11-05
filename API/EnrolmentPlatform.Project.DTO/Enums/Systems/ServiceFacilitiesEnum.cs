using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    /// <summary>
    /// 周
    /// </summary>
    public enum WeekDayEnum
    {
        /// <summary>
        /// 周一
        /// </summary>
        [Description("周一")]
        Monday = 1,
        /// <summary>
        /// 周二
        /// </summary>
        [Description("周二")]
        Tuesday = 2,
        /// <summary>
        /// 周三
        /// </summary>
        [Description("周三")]
        Wednesday = 3,
        /// <summary>
        /// 周四
        /// </summary>
        [Description("周四")]
        Thursday = 4,
        /// <summary>
        /// 周五
        /// </summary>
        [Description("周五")]
        Friday = 5,
        /// <summary>
        /// 周六
        /// </summary>
        [Description("周六")]
        Saturday = 6,
        /// <summary>
        /// 周一
        /// </summary>
        [Description("周日")]
        Sunday = 7
    }
}
