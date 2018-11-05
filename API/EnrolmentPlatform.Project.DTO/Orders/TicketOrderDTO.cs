using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    /// <summary>
    /// 门票订单搜索
    /// </summary>
    public class TicketOrderSearchParamDTO : GridDataRequest
    {
        /// <summary>
        /// 供应商ID
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 出票状态
        /// </summary>
        [DataMember]
        public int TicketStatus { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        [DataMember]
        public int SupplierType { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 取票人
        /// </summary>
        [DataMember]
        public string TicketCollector { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [DataMember]
        public Guid AccountId { get; set; }

        /// <summary>
        /// 创建方
        /// </summary>
        //[DataMember]
        //public int CreateForSystem { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int OrderSource { get; set; }
    }
    [Serializable]
    [DataContract]
    public class TicketOrderInfo
    {
        [DataMember]
        /// <summary>
        /// 订单项ID
        /// </summary>
        public Guid OrderId { get; set; }
        [DataMember]
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string OrderNo { get; set; }
        [DataMember]
        /// <summary>
        /// 产品信息
        /// </summary>
        public string OrderName { get; set; }
        [DataMember]
        /// <summary>
        /// 数量
        /// </summary>
        public int TotalQuantity { get; set; }
        [DataMember]
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 总金额格式化
        /// </summary>
        [DataMember]
        public string TotalAmountCH
        {
            get
            {
                string res = string.Empty;
                res = this.UpdateTotalAmount.ToString("#0.00");
                return "¥" + res;
            }
        }
        [DataMember]
        /// <summary>
        /// 修改后的金额
        /// </summary>
        public decimal UpdateTotalAmount { get; set; }
        [DataMember]
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单状态中文
        /// </summary>
        [DataMember]
        public string OrderStatusCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderStatusForTicketEnum)this.OrderStatus);
            }
        }
        [DataMember]
        /// <summary>
        /// 游玩日期
        /// </summary>
        public DateTime PlayDay { get; set; }
        /// <summary>
        /// 门票状态
        /// </summary>
        [DataMember]
        public int TicketStatus { get; set; }
        /// <summary>
        /// 门票状态中文
        /// </summary>
        [DataMember]
        public string TicketStatusCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((TicketStatusEnum)this.TicketStatus);
            }
        }
        [DataMember]
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
        [DataMember]
        /// <summary>
        /// 下单人
        /// </summary>
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        [DataMember]
        public int SupplierType { get; set; }
        [DataMember]
        /// <summary>
        /// 供应商类型中文
        /// </summary>
        public string SupplierTypeCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((SupplierTypeEnum)this.SupplierType);
            }
        }
        [DataMember]
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 取消方
        /// </summary>
        [DataMember]
        public int CancelForSystem { get; set; }
        /// <summary>
        /// 订单实际金额
        /// </summary>
        [DataMember]
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        [DataMember]
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int CreateForSystem { get; set; }

        [DataMember]
        public string CreateForSystemCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((SystemTypeEnum)this.CreateForSystem);
            }
        }
        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int OrderSource { get; set; }
        [DataMember]
        public string OrderSourceCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderSourceEnum)this.OrderSource);
            }
        }
    }

    /// <summary>
    /// 交易明细请求类
    /// </summary>
    public class SearchTransactionDetailsDto : GridDataRequest
    {
        /// <summary>
        ///  交易类型
        /// </summary>
        public TransactionClassifyEnum? TransactionClassify { get; set; }

        /// <summary>
        ///  开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 支付方式  
        /// </summary>
        public OrderPayClassifyEnum? PaymentMethod { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TransactionNo { get; set; }



    }

    /// <summary>
    /// 交易明细 实体  
    /// </summary>
    public class TransactionDetailsDto
    {
        /// <summary>
        /// 金额 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public TransactionClassifyEnum TransactionClassify { get; set; }

        public string TransactionClassifyName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(TransactionClassify);
            }
        }

        /// <summary>
        /// 支付方式 
        /// </summary>
        public OrderPayClassifyEnum? PaymentMethod { get; set; }


        public string PaymentMethodName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(PaymentMethod);
            }
        }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        public string TransactionNo { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

    }

    /// <summary>
    /// 游客服务中心  账户明细
    /// </summary>
    public class TouristsAccountDetailsDto
    {
        /// <summary>
        /// 收款合计
        /// </summary>
        public decimal ReceiptTotal { get; set; }

        /// <summary>
        /// 退款合计
        /// </summary>
        public decimal RefundTotal { get; set; }
    }


}
