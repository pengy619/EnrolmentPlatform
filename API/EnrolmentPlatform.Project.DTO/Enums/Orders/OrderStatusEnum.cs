using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 农产品订单状态
    /// </summary>
    public enum OrderStatusForSpecialtyEnum
    {
        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        Unpaid = 1,
        /// <summary>
        /// 待发货
        /// </summary>
        [Description("待发货")]
        UnSend = 2,
        /// <summary>
        /// 待收货
        /// </summary>
        [Description("待收货")]
        UnReceived = 3,
        /// <summary>
        /// 已付定金
        /// </summary>
        [Description("已付定金")]
        PayDeposit = 4,
        /// <summary>
        /// 待付尾款
        /// </summary>
        [Description("待付尾款")]
        UnRetainage = 5,
        /// <summary>
        /// 退款中
        /// </summary>
        [Description("退款中")]
        Refunding = 6,
        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refunded = 7,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 8,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canceled = 9,
        /// <summary>
        /// 已收货
        /// </summary>
        [Description("已收货")]
        Received = 10,
    }

    /// <summary>
    /// 门票订单状态
    /// </summary>
    public enum OrderStatusForTicketEnum
    {
        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        Unpaid = 1,
        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Paid = 2,
        /// <summary>
        /// 退款中
        /// </summary>
        [Description("退款中")]
        Refunding = 6,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 8,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canceled = 9,
        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refunded = 7,
    }

    public enum OrderPayStatusEnum
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
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refunded = 3,
    }

    public enum OrderCancelStatusEnum
    {
        /// <summary>
        /// 未取消
        /// </summary>
        [Description("未取消")]
        UnCancel = 1,

        /// <summary>
        /// 取消待审核
        /// </summary>
        [Description("取消待审核")]
        Canceling = 2,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canceled = 3,

        /// <summary>
        /// 取消失败
        /// </summary>
        [Description("取消失败")]
        CancelFail = 4,
    }

    public enum OrderSettlementStatusEnum
    {
        /// <summary>
        /// 未结算
        /// </summary>
        [Description("未结算")]
        UnSettlement = 1,

        /// <summary>
        /// 已结算
        /// </summary>
        [Description("已结算")]
        Settlemented = 2
    }

    public enum OrderPayClassifyEnum
    {
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChatPay = 1,
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 2,

        /// <summary>
        /// 现金
        /// </summary>
        [Description("现金")]
        Cash = 3
    }
    public enum OrderClassifyEnum
    {
        /// <summary>
        /// 农产品订单
        /// </summary>
        [Description("农产品")]
        Specialty = 1,
        /// <summary>
        /// 门票订单
        /// </summary>
        [Description("门票")]
        Ticket = 2,
        /// <summary>
        /// 餐饮订单
        /// </summary>
        [Description("餐饮")]
        Catering = 3
    }

    /// <summary>
    /// 取消方
    /// </summary>
    public enum CancelForSystemEnum
    {
        /// <summary>
        /// 景区运营端
        /// </summary>
        [Description("景区运营端")]
        Scenic = 1,
        /// <summary>
        /// 用户端
        /// </summary>
        [Description("用户端")]
        Consumer = 2,
        /// <summary>
        /// 供应商端
        /// </summary>
        [Description("供应商端")]
        Supplier = 3,
        /// <summary>
        /// 分销商端
        /// </summary>
        [Description("分销商端")]
        Distributor = 4,
        /// <summary>
        /// 游客服务中心
        /// </summary>
        [Description("游客服务中心")]
        TouristServiceCenter = 5
    }
    /// <summary>
    /// 订单来源
    /// </summary>
    public enum OrderSourceEnum
    {
        /// <summary>
        /// H5
        /// </summary>
        [Description("H5")]
        H5 = 1,
        /// <summary>
        /// B2C
        /// </summary>
        [Description("B2C")]
        B2C = 2,
        /// <summary>
        /// <summary>
        /// 自助服务终端机
        /// </summary>
        [Description("自助服务终端机")]
        SelfServiceTerminal = 3,
        /// <summary>
        /// 游客服务中心
        /// </summary>
        [Description("游客服务中心")]
        TouristServiceCenter = 4
    }
    /// <summary>
    /// 取票状态
    /// </summary>
    public enum TicketStatusEnum
    {
        /// <summary>
        /// 未出票
        /// </summary>
        [Description("未出票")]
        NotTicket = 1,
        /// <summary>
        /// 已出票
        /// </summary>
        [Description("已出票")]
        AlreadyTicket = 2,
    }

    /// <summary>
    /// 餐饮订单状态
    /// </summary>
    public enum OrderStatusForCateringEnum
    {
        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        Unpaid = 1,
        /// <summary>
        /// 待消费
        /// </summary>
        [Description("待消费")]
        UnConsume = 2,
        /// <summary>
        /// 退款中
        /// </summary>
        [Description("退款中")]
        Refunding = 6,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 8,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canceled = 9,
        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refunded = 7
    }
}
