using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Finance
{
    /// <summary>
    ///  提现 审核状态 
    /// </summary>
    public enum CashApplyStatusEnum
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Checking = 0,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Agree = 1,

        /// <summary>
        /// 拒绝提现
        /// </summary>
        [Description("审核拒绝")]
        Refuse = 2,


    }
}
