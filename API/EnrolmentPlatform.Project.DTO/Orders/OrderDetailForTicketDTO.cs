using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Product;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    public class OrderDetailForTicketDTO
    {
        [DataMember]
        /// <summary>
        /// 订单基本信息
        /// </summary>
        public OrderBasicInfo_TicketDetail OrderBasic { get; set; }
        [DataMember]
        /// <summary>
        /// 门票订单信息
        /// </summary>
        public Order_TicketDetail OrderForTicket { get; set; }
        [DataMember]
        /// <summary>
        /// 订单单据信息
        /// </summary>
        public List<OrderBill_TicketDetail> OrderBillList { get; set; }
        [DataMember]
        /// <summary>
        /// 游客
        /// </summary>
        public List<OrderTourist_TicketDetail> TouristList { get; set; }
        /// <summary>
        /// 订单核销信息集合
        /// </summary>
        [DataMember]
        public List<OrderVerification_TicketDetail> VerificationForTicketList { get; set; }
        /// <summary>
        /// 订单流程集合
        /// </summary>
        [DataMember]
        public List<OrderProcess_TicketDetail> OrderProcessList { get; set; }

        /// <summary>
        /// 门票列表
        /// </summary>
        [DataMember]
        public List<OrderTicketToken_TicketDetail> TicketTokenList { get; set; }

        /// <summary>
        /// 待支付剩余时间
        /// </summary>
        [DataMember]
        public string PaySurplusTime { get; set; }

        /// <summary>
        /// 自动取消时间
        /// </summary>
        [DataMember]
        public DateTime AutoCancelDate { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        [DataMember]

        public string ProductPhoto { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        [DataMember]
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }

        /// <summary>
        /// 是否可退
        /// </summary>
        [DataMember]
        public bool IsRefund
        {
            get
            {
                bool res = true;
                if (!(this.OrderBasic.PayStatus == (int)OrderPayStatusEnum.Paid))
                {
                    return false;
                }
                //已完成的和退款中的不能退
                if (this.OrderBasic.OrderStatus == (int)OrderStatusForTicketEnum.Completed || this.OrderBasic.OrderStatus == (int)OrderStatusForTicketEnum.Refunding|| this.OrderBasic.OrderStatus == (int)OrderStatusForTicketEnum.Refunded)
                {
                    res = false;
                }
                //退票规则
                if (this.OrderForTicket.RefundRule == (int)RefundPriceEnum.No)
                {
                    res = false;
                }
                else
                {
                    //游玩前
                    if (this.OrderForTicket.IsBefore)
                    {
                        if (this.OrderForTicket.PlayDay.AddDays(-(this.OrderForTicket.RefundDay - 1)) <= DateTime.Today)
                        {
                            res = false;

                        }
                    }
                    else
                    {
                        if (this.OrderForTicket.PlayDay.AddDays(this.OrderForTicket.RefundDay + 1) <= DateTime.Today)
                        {
                            res = false;
                        }
                    }
                }
                return res;
            }
        }
    }
    public class OrderBasicInfo_TicketDetail
    {
        /// <summary>
        /// 订单名称
        /// </summary> 
        [DataMember]
        public string OrderName { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary> 
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单分类【1：农产品】【2：门票】
        /// </summary> 
        [DataMember]
        public int Classify { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary> 
        [DataMember]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary> 
        [DataMember]
        public decimal PreferentialAmount { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary> 
        [DataMember]
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 修改后总金额
        /// </summary> 
        [DataMember]
        public decimal UpdateTotalAmount { get; set; }
        /// <summary>
        /// 总运费
        /// </summary> 
        [DataMember]
        public decimal TotalCostsAmount { get; set; }
        /// <summary>
        /// 总数量
        /// </summary> 
        [DataMember]
        public int TotalQuantity { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary> 
        [DataMember]
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary> 
        [DataMember]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 处理理由
        /// </summary> 
        [DataMember]
        public string OptionReason { get; set; }
        /// <summary>
        /// 订单状态【1：待支付】【2：待发货】【3：待收货】【4：已付定金】【5：代付尾款】【6：退款中】【7：已退款】【8：已完成】【9：已取消】
        /// </summary> 
        [DataMember]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 上一次订单状态
        /// </summary>
        [DataMember]
        public int BeforeOrderStatus { get; set; }
        /// <summary>
        /// 取消状态【1：未取消】【2：取消待审核】【3：已取消】【4：取消失败】
        /// </summary> 
        [DataMember]
        public int CancelStatus { get; set; }
        /// <summary>
        /// 支付状态【1：待支付】【2：已支付】
        /// </summary> 
        [DataMember]
        public int PayStatus { get; set; }
        /// <summary>
        /// 结算状态 【1：未结算】【2：已结算】
        /// </summary> 
        [DataMember]
        public int SettlementStatus { get; set; }
        /// <summary>
        /// 配送方式【1：快递配送】【2：上门自取】
        /// </summary> 
        [DataMember]
        public int Deliveries { get; set; }
        /// <summary>
        /// 支付方式【1：微信支付】【2：支付宝支付】
        /// </summary> 
        [DataMember]
        public int PayClassify { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary> 
        [DataMember]
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary> 
        [DataMember]
        public string PayTransactionNo { get; set; }
        /// <summary>
        /// 取消流水号
        /// </summary> 
        [DataMember]
        public string CancelTransactionNo { get; set; }
        /// <summary>
        /// 快递公司
        /// </summary> 
        [DataMember]
        public string ExpressCompany { get; set; }
        /// <summary>
        /// 运单号
        /// </summary> 
        [DataMember]
        public string WaybillNumber { get; set; }
        /// <summary>
        /// 收货人
        /// </summary> 
        [DataMember]
        public string Consignee { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary> 
        [DataMember]
        public string ConsigneeAddress { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary> 
        [DataMember]
        public string ConsigneePhone { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary> 
        [DataMember]
        public DateTime DeliverGoodsTime { get; set; }
        /// <summary>
        /// 申请取消时间
        /// </summary> 
        [DataMember]
        public DateTime ApplyCancelTime { get; set; }
        /// <summary>
        /// 取消处理时间
        /// </summary> 
        [DataMember]
        public DateTime OptionCancelTime { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary> 
        [DataMember]
        public DateTime SendProductTime { get; set; }
        /// <summary>
        /// 取消方
        /// </summary>
        [DataMember]
        public int CancelForSystem { get; set; }
        /// <summary>
        /// 下单方
        /// </summary>
        [DataMember]
        public int CreateForSystem { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int OrderSource { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }


    public class Order_TicketDetail
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
        [DataMember]
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }
        [DataMember]
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNo { get; set; }
        [DataMember]
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        [DataMember]
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        [DataMember]
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        [DataMember]
        /// <summary>
        /// 数量
        /// </summary>
        public int ProductQuantity { get; set; }
        [DataMember]
        /// <summary>
        /// 票种
        /// </summary>
        public string ProductCategory { get; set; }
        [DataMember]
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Amount { get; set; }
        [DataMember]
        /// <summary>
        /// 状态：【1:未出票】【2：已出票】
        /// </summary>
        public int Status { get; set; }
        [DataMember]
        /// <summary>
        /// 游玩日期
        /// </summary>
        public DateTime PlayDay { get; set; }
        [DataMember]
        /// <summary>
        /// 退票规则【1：不可退】【2：条件退】
        /// </summary>
        public int RefundRule { get; set; }
        [DataMember]
        /// <summary>
        ///  退天
        /// </summary>
        public int RefundDay { get; set; }
        [DataMember]
        /// <summary>
        /// 游玩日期前 是否
        /// </summary>
        public bool IsBefore { get; set; }
        /// <summary>
        /// 票务类型：【1：单票】【2：套票】
        /// </summary>
        [DataMember]
        public int? TicketClassify { get; set; }

        /// <summary>
        /// 取票码
        /// </summary>
        [DataMember]
        public string UnifiedCheckCode { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
    }

    public class OrderProcess_TicketDetail

    {
        /// <summary>
        /// 订单唯一ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        [DataMember]
        public string ProcessName { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 流程类型
        /// </summary>
        [DataMember]
        public int ProcessClassify { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
    }

    public class OrderBill_TicketDetail
    {
        [DataMember]
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        [DataMember]
        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillNo { get; set; }
        [DataMember]
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        [DataMember]
        /// <summary>
        /// 交易类型
        /// </summary>
        public int TransactionClassify { get; set; }
        [DataMember]
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TranscationNo { get; set; }
        [DataMember]
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayClassify { get; set; }
        [DataMember]
        /// <summary>
        /// 支付状态
        /// </summary>
        public int PayStatus { get; set; }
        [DataMember]
        /// <summary>
        /// 单据类型
        /// </summary>
        public int BillClassify { get; set; }
        [DataMember]
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
    }

    public class OrderTourist_TicketDetail
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单明细ID
        /// </summary>
        [DataMember]
        public Guid OrderDetailId { get; set; }
        /// <summary>
        /// 游客姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string Sex { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        [DataMember]
        public int CredentialsType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        [DataMember]
        public string Credentials { get; set; }
        /// <summary>
        /// 是否为取票人
        /// </summary>
        [DataMember]
        public bool IsTicket { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
    }

    public class OrderVerification_TicketDetail
    {
        /// <summary>
        /// 门票ID
        /// </summary>
        [DataMember]
        public Guid TicketTokenId { get; set; }

        [DataMember]
        /// <summary>
        /// 核销编号 保证唯一
        /// </summary>
        public string VerificationNo { get; set; }
        [DataMember]
        /// <summary>
        /// 类型：【1：景点】【2：游乐项目】
        /// </summary>
        public int Classify { get; set; }
        [DataMember]
        /// <summary>
        /// 景点项目名称
        /// </summary>
        public string ScenicProjectName { get; set; }
        [DataMember]
        /// <summary>
        /// 景点项目Id
        /// </summary>
        public Guid ScenicProjectId { get; set; }
        [DataMember]
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNo { get; set; }
        [DataMember]
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        [DataMember]
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        [DataMember]
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }
        [DataMember]
        /// <summary>
        /// 激活码
        /// </summary>
        public string ActivationCode { get; set; }
        [DataMember]
        /// <summary>
        /// 状态：【1：待核销（订单已支付）】【2：已锁定（订单退款中）】【3：已核销】【4：已失效（订单已退款、订单过游玩日期、供应商手动失效）】
        /// </summary>
        public int Status { get; set; }
        [DataMember]
        /// <summary>
        /// 有效期：游玩日期
        /// </summary>
        public DateTime ExpiryDate { get; set; }
        [DataMember]
        /// <summary>
        /// 核销方式:【1：身份证】
        /// </summary>
        public int Pattern { get; set; }
        [DataMember]
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
    }

    public class OrderTicketToken_TicketDetail
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 门票订单ID
        /// </summary>
        [DataMember]
        public Guid SubOrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }

        /// <summary>
        /// 取票码
        /// </summary>
        [DataMember]
        public string TicketToken { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 游客ID
        /// </summary>
        [DataMember]
        public Guid TouristId { get; set; }

        /// <summary>
        /// 游玩日期
        /// </summary>
        [DataMember]
        public DateTime PlayDay { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
    }
}
