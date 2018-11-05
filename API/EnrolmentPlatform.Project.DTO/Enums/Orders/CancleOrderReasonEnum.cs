using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 供应商取消原因
    /// </summary>
    public enum SupplierCancleOrderReasonEnum
    {
        [Description("产能不足")]
        CapacityNotenough = 1,
        [Description("其他")]
        Other = 2,
    }
    /// <summary>
    /// 景区取消原因
    /// </summary>
    public enum ScenicCancleOrderReasonEnum
    {
        [Description("产能不足")]
        CapacityNotenough = 1,
        [Description("其他")]
        Other = 2,
    }
    /// <summary>
    /// 游客服务中心取消订单原因
    /// </summary>
    public enum TouristCenterCancleOrderReasonEnum
    {
        [Description("产能不足")]
        CapacityNotenough = 1,
        [Description("其他")]
        Other = 2,
    }
    /// <summary>
    /// 消费者取消原因
    /// </summary>
    public enum ConsumerCancleOrderReasonEnum
    {
        [Description("不想买了")]
        NoBuy = 1,
        [Description("其他")]
        Other = 2,
    }
    /// <summary>
    /// 消费者退换货原因
    /// </summary>
    public enum ConsumerRefundableReasonEnum
    {
        [Description("商品损坏")]
        Damage = 1,
        [Description("其他")]
        Other = 2,
    }
}
