using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    public class SaveSpecialtyOrderDTO
    {
        [DataMember]
        /// <summary>
        /// 下单人ID
        /// </summary>
        public Guid AccountId { get; set; }
        [DataMember]
        /// <summary>
        /// 农产品集合
        /// </summary>
        public List<SpecialtyProductInfo> ProductList { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 收货地址Id
        /// </summary>
        [DataMember]
        public Guid DeliveryAddressId { get; set; }
        /// <summary>
        /// 下单方
        /// </summary>
        [DataMember]
        public int CreateForSystem { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public List<OrderRemark> OrderRemark { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int OrderSource { get; set; }

        /// <summary>
        /// 订金
        /// </summary>
        [DataMember]
        public decimal DepositPrice { get; set; }
    }
    [Serializable]
    [DataContract]
    public class SpecialtyProductInfo
    {
        [DataMember]
        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductId { get; set; }
        [DataMember]
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 小计金额
        /// </summary>
        public decimal SubTotalAmount { get; set; }
        /// <summary>
        /// 库存编号
        /// </summary>
        public Guid InventoryId { get; set; }

    }

    /// <summary>
    /// 订单备注
    /// </summary>
    [Serializable]
    [DataContract]
    public class OrderRemark
    {
        /// <summary>
        /// 供应商ID
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}
