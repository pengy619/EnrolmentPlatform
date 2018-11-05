using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Enterprise
{
    /// <summary>
    /// 结算周期
    /// </summary>
    public enum SettlementCycleEnum
    {
        /// <summary>
        /// 及时
        /// </summary>
        [Description("及时")]
        Now = 1,

        /// <summary>
        /// 周结
        /// </summary>
        [Description("周结")]
        Week = 2,

        /// <summary>
        /// 月结
        /// </summary>
        [Description("月结")]
        Moon = 3,
    }
}
