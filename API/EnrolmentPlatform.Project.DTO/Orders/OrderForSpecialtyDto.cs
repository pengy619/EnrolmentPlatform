using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 订单搜索dto
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchParamForSpecialtyOrderDTO : GridDataRequest
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
        /// 销售模式
        /// </summary>
        [DataMember]
        public int SalesModel { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        //[DataMember]
        //public int CreateForSystem { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int OrderSource { get; set; }

    }
    /// <summary>
    /// 农产品订单dto
    /// </summary>
    public class OrderForSpecialtyDto
    {
        /// <summary>
        /// 订单项ID
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 销售模式
        /// </summary>
        public int SalesModel { get; set; }
        public string SalesModelCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ProductSaleModelEnum)this.SalesModel);
            }
        }
        /// <summary>
        /// 产品信息
        /// </summary>
        public string OrderName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int TotalQuantity { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        ///// <summary>
        ///// 格式化金额
        ///// </summary>
        //public string TotalAmountCH {
        //    get
        //    {
        //        string res = string.Empty;
        //        res = this.UpdateTotalAmount.ToString("#0.00");
        //        return "¥" + res;
        //    }
        //}
        /// <summary>
        /// 修改后的金额
        /// </summary>
        public decimal UpdateTotalAmount { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 中文订单状态
        /// </summary>
        public string OrderStatusCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderStatusForSpecialtyEnum)this.OrderStatus);
            }
        }
       
        /// <summary>
        /// 下单人
        /// </summary>
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 实际支付金额
        /// </summary>
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

        /// <summary>
        /// 取消方
        /// </summary>
        [DataMember]
        public int CancelForSystem { get; set; }
    }


    [Serializable]
    [DataContract]
    public class UpdateSpecialtyOrderPriceDTO: BaseOrderOperationDTO
    {
        /// <summary>
        /// 修改总金额
        /// </summary>
        [DataMember]
        public decimal UpdateTotalAmount { get; set; }
    }
}
