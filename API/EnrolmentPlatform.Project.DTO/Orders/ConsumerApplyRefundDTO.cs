using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 消费者申请退款DTO
    /// </summary>
   public class ConsumerApplyRefundDTO
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 退款理由
        /// </summary>
        [DataMember]
        public string RefundReason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public Guid AccountId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string AccountName { get; set; }
    }
}
