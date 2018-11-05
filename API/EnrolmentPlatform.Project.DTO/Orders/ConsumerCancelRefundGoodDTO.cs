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
    /// 消费者取消退换货
    /// </summary>
    public class ConsumerCancelRefundGoodDTO : BasePostOperation
    {
        [DataMember]
        /// <summary>
        /// 退换货ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 理由
        /// </summary>
        [DataMember]
        public string Reason { get; set; }

        /// <summary>
        /// 详细理由
        /// </summary>
        [DataMember]
        public string ReasonDetail { get; set; }
    }
}
