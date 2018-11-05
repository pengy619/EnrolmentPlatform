using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 单据支付状态
    /// </summary>
    public enum BillPayStatusEnum
    {
        /// <summary>
        /// 未支付
        /// </summary>
        [Description("未支付")]
        Unpaid = 1,

        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Paid = 2,
        /// <summary>
        /// 已失效
        /// </summary>
        [Description("已失效")]
        Overdue = 3,
    }
    /// <summary>
    /// 单据交易类型
    /// </summary>
    public enum TransactionClassifyEnum
    {
        /// <summary>
        /// 订单收款
        /// </summary>
        [Description("订单收款")]
        Payment = 1,

        /// <summary>
        /// 订单退款
        /// </summary>
        [Description("订单退款")]
        Refund = 2
    }
    /// <summary>
    /// 单据类型
    /// </summary>
    public enum BillClassifyEnum
    {
        /// <summary>
        /// 一次全款支付
        /// </summary>
        [Description("一次全款支付")]
        OncePay = 1,

        /// <summary>
        /// 多次支付
        /// </summary>
        [Description("多次支付")]
        ManyPay = 2
    }
    public enum BillTransactionClassifyEnum
    {
        /// <summary>
        /// 支付
        /// </summary>
        [Description("支付")]
        Pay = 1,
        /// <summary>
        /// 退款
        /// </summary>
        [Description("退款")]
        Refund = 2
    }
}
