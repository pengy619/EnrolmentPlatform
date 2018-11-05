using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 餐饮订单DTO
    /// </summary>
    public class SearchParamForCateringOrderDTO : GridDataRequest
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int OrderStatus { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

    }
    public class OrderForCateringDTO
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int OrderStatus { get; set; }

        /// <summary>
        /// 中文订单状态
        /// </summary>
        [DataMember]
        public string OrderStatusCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderStatusForCateringEnum)this.OrderStatus);
            }
        }

        /// <summary>
        /// 实际支付金额
        /// </summary>
        [DataMember]
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 修改后的金额
        /// </summary>
        [DataMember]
        public decimal UpdateTotalAmount { get; set; }

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

        /// <summary>
        /// 下单人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int CreateForSystem { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        [DataMember]
        public decimal PaidAmount { get; set; }


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

        /// <summary>
        /// 取消方
        /// </summary>
        [DataMember]
        public int CancelForSystem { get; set; }
    }
}
