using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    public class OrderBasicDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }
        /// <summary>
        /// 订单分类
        /// </summary>
        public int Classify { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal PreferentialAmount { get; set; }
        /// <summary>
        ///实际金额
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        ///修改后总金额
        /// </summary>
        public decimal UpdateTotalAmount { get; set; }
        /// <summary>
        ///总运费
        /// </summary>
        public decimal TotalCostsAmount { get; set; }
        /// <summary>
        ///总数量
        /// </summary>
        public int TotalQuantity { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 处理理由
        /// </summary>
        public string OptionReason { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        ///上一 订单状态
        /// </summary>
        public int BeforeOrderStatus { get; set; }
        /// <summary>
        ///取消状态
        /// </summary>
        public int CancelStatus { get; set; }
        /// <summary>
        ///支付状态
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        ///结算状态
        /// </summary>
        public int SettlementStatus { get; set; }
        /// <summary>
        /// 支付交易号
        /// </summary>
        public string PayTransactionNo { get; set; }
        /// <summary>
        /// 取消交易号
        /// </summary>
        public string CancelTransactionNo { get; set; }
    }

    /// <summary>
    /// H5用户中心订单概况
    /// </summary>
    public class H5UserCenterOrderComm
    {
        /// <summary>
        /// 待付款订单数量
        /// </summary>
        public int UnPayCount { set; get; }

        /// <summary>
        /// 待收货订单数量
        /// </summary>
        public int UnReiceiveCount { set; get; }

        /// <summary>
        /// 退换货订单数量
        /// </summary>
        public int RefundCount { set; get; }
    }
}
