using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    /// <summary>
    /// 出票
    /// </summary>
    public class TicketingDTO : BasePostOperation
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
    }
    [Serializable]
    [DataContract]
    /// <summary>
    /// 游客服务中心退款DTO
    /// </summary>
    public class TouristCenterRefundDTO : BasePostOperation
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        [DataMember]
        public string Reason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        [DataMember]
        public decimal RefundAmount { get; set; }
    }
}
