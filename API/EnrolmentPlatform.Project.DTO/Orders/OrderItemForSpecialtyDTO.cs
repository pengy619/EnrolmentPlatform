using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 农场品退换货商品
    /// </summary>
    public class OrderItemForSpecialtyDTO
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
        /// 产品ID
        /// </summary> 
        [DataMember]
        public Guid ProductID { get; set; }
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
        public decimal SubTotalAmount { get; set; }
        /// <summary>
        /// 定金比例
        /// </summary> 
        [DataMember]
        public decimal DepositRatio { get; set; }
        /// <summary>
        /// 定金
        /// </summary> 
        [DataMember]
        public decimal DepositPrice { get; set; }
        /// <summary>
        /// 尾款
        /// </summary> 
        [DataMember]
        public decimal RetainagePrice { get; set; }
        /// <summary>
        /// 运费
        /// </summary> 
        [DataMember]
        public decimal ExpressFee { get; set; }
        /// <summary>
        /// 品种
        /// </summary> 
        [DataMember]
        public string Varieties { get; set; }
        /// <summary>
        /// 规格
        /// </summary> 
        [DataMember]
        public string SpecsStr { get; set; }
        /// <summary>
        /// 单位
        /// </summary> 
        [DataMember]
        public string SalesUnit { get; set; }
        /// <summary>
        /// 销售模式
        /// </summary> 
        [DataMember]
        public int SalesModel { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary> 
        [DataMember]
        public string ProductNo { get; set; }
        /// <summary>
        /// 产品分类名称
        /// </summary> 
        [DataMember]
        public string CategoryName { get; set; }
        /// <summary>
        /// 代付尾款时间
        /// </summary> 
        [DataMember]
        public DateTime? PayRetainageTime { get; set; }

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
        /// 产品图片
        /// </summary>
        [DataMember]
        public string Photo { get; set; }

        /// <summary>
        /// 产品已退数量
        /// </summary>
        [DataMember]
        public int RetreatedQuantity { get; set; }

    }
}
