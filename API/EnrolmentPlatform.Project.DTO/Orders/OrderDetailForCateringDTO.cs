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
    public class OrderDetailForCateringDTO
    {
        [DataMember]
        /// <summary>
        /// 订单基本信息
        /// </summary>
        public OrderBasicInfo_CateringDetail OrderBasic { get; set; }

        /// <summary>
        /// 餐饮订单信息
        /// </summary>
        [DataMember]
        public OrderForCatering_CateringDetail OrderForCatering { get; set; }

        /// <summary>
        /// 餐饮核销信息
        /// </summary>
        [DataMember]
        public List<VerificationForCatering_Detail> VerificationForCateringList { get; set; }

        [DataMember]
        /// <summary>
        /// 订单单据信息
        /// </summary>
        public List<OrderBill_CateringDetail> OrderBillList { get; set; }

        /// <summary>
        /// 订单流程集合
        /// </summary>
        [DataMember]
        public List<OrderProcess_CateringDetail> OrderProcessList { get; set; }

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
                if (this.OrderBasic.OrderStatus == (int)OrderStatusForCateringEnum.Completed || this.OrderBasic.OrderStatus == (int)OrderStatusForCateringEnum.Refunding || this.OrderBasic.OrderStatus == (int)OrderStatusForCateringEnum.Refunded)
                {
                    res = false;
                }
                //退款规则
                if (this.OrderForCatering.RefundRule == (int)RefundPriceEnum.No)
                {
                    res = false;
                }
                else
                {
                    if (this.OrderForCatering.EndExpiryDate < DateTime.Today)
                    {
                        res = false;
                    }

                }
                return res;
            }
        }

    }
    public class OrderBasicInfo_CateringDetail
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

    public class VerificationForCatering_Detail
    {
        /// <summary>
        /// 订单Id
        /// </summary> 
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单子Id
        /// </summary> 
        [DataMember]
        public Guid SubOrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary> 
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 消费码
        /// </summary> 
        [DataMember]
        public string CateringToken { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary> 
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary> 
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 有效期
        /// </summary> 
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        ///  状态
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 核销方式
        /// </summary> 
        [DataMember]
        public int Pattern { get; set; }

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

    }
    public class OrderProcess_CateringDetail

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
    }

    public class OrderBill_CateringDetail
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
    }

    public class OrderForCatering_CateringDetail
    {
        /// <summary>
        /// 订单Id 
        /// </summary> 
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary> 
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 产品编号 
        /// </summary> 
        [DataMember]
        public string ProductNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary> 
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary> 
        [DataMember]
        public string ProductImg { get; set; }
        /// <summary>
        /// 主产品Id
        /// </summary> 
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 子产品Id
        /// </summary> 
        [DataMember]
        public Guid SubProductId { get; set; }
        /// <summary>
        /// 单价
        /// </summary> 
        [DataMember]
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary> 
        [DataMember]
        public int Quantity { get; set; }
        /// <summary>
        /// 小计 
        /// </summary> 
        [DataMember]
        public decimal SubTotalPrice { get; set; }
        /// <summary>
        /// 退票规则
        /// </summary> 
        [DataMember]
        public int RefundRule { get; set; }
        /// <summary>
        /// 开始有效期
        /// </summary> 
        [DataMember]
        public DateTime StartExpiryDate { get; set; }
        /// <summary>
        /// 结束有效期
        /// </summary> 
        [DataMember]
        public DateTime EndExpiryDate { get; set; }
        /// <summary>
        ///  统一票码
        /// </summary> 
        [DataMember]
        public string UnifiedCheckCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [DataMember]
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
    }
}
