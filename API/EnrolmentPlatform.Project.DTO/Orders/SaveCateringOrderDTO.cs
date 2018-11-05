using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 保存餐饮订单DTO
    /// </summary>
    [Serializable]
    [DataContract]
    public class SaveCateringOrderDTO
    {
        [DataMember]
        /// <summary>
        /// 下单人ID
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 下单方
        /// </summary>
        [DataMember]
        public int CreateForSystem { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        [DataMember]
        public Guid ShopId { get; set; }

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
}
