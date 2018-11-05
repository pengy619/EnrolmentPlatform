using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 游客收货DTO
    /// </summary>
    public class ConsumerReceivedGoodDTO
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
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
