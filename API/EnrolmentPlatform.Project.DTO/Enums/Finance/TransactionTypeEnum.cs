using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Finance
{

    public enum TransactionTypeEnum
    {
        /// <summary>
        /// 订单结算
        /// </summary>
        [Description("订单结算")]
        OrderSettlement = 1,

        /// <summary>
        /// 账户提现
        /// </summary>
        [Description("账户提现")]
        Withdraw = 2
    }
    public enum SettlementStatusEnum
    {

        /// <summary>
        /// 待结算
        /// </summary>
        [Description("待结算")]
        UnSettlement = 1,


        /// <summary>
        /// 已结算
        /// </summary>
        [Description("已结算")]
        Settlemented = 2
    }
}
