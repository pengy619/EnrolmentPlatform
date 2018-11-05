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
    [Serializable]
    [DataContract]
    public class SearchParamForH5Order : GridDataRequest
    {
        /// <summary>
        /// 订单类型
        /// </summary>
        [DataMember]
        public int OrderClassify { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [DataMember]
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 订单列表
        /// </summary>
        [DataMember]
        public List<H5OrderInfo> OrderList { get; set; }

    }
    [Serializable]
    [DataContract]
    public class H5OrderInfo
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
        /// 实际金额
        /// </summary>
        [DataMember]
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        [DataMember]
        public int OrderClassify { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        [DataMember]
        public string OrderClassifyCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderClassifyEnum)this.OrderClassify);
            }
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public string OrderStatusCH
        {
            get
            {
                string status = string.Empty;
                if (this.OrderClassify == (int)OrderClassifyEnum.Specialty)
                {
                    return EnumDescriptionHelper.GetDescription((OrderStatusForSpecialtyEnum)this.OrderStatus);
                }
                else if (this.OrderClassify == (int)OrderClassifyEnum.Ticket)
                {
                    return EnumDescriptionHelper.GetDescription((OrderStatusForTicketEnum)this.OrderStatus);
                }
                return status;
            }
        }

        /// <summary>
        /// 取消方
        /// </summary>
        [DataMember]
        public int CancelForSystem { get; set; }

        /// <summary>
        /// 销售模式
        /// </summary>
        [DataMember]
        public int SalesModel { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 农场品信息
        /// </summary>
        [DataMember]
        public List<SpecialtyProduct_OrderInfo> SpecialtyProduct { get; set; }

        /// <summary>
        /// 门票产品信息
        /// </summary>
        [DataMember]
        public List<TicketProduct_OrderInfo> TicketProduct { get; set; }
    }
    [Serializable]
    [DataContract]

    public class SpecialtyProduct_OrderInfo
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 产品信息
        /// </summary>
        [DataMember]
        public string ProductInfo { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        [DataMember]
        public string ProductPhoto { get; set; }
    }

    [Serializable]
    [DataContract]
    public class TicketProduct_OrderInfo
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 产品信息
        /// </summary>
        [DataMember]
        public string ProductInfo { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        [DataMember]
        public string ProductPhoto { get; set; }
    }
}
