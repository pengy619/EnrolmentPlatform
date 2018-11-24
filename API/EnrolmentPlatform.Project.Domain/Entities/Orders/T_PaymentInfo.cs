using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Orders
{
    /// <summary>
    /// 订单缴费明细
    /// </summary>
    [Serializable]
    [Table("T_PaymentInfo")]
    [DataContract]
    public class T_PaymentInfo : Entity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { set; get; }

        /// <summary>
        /// 缴费金额
        /// </summary>
        [DataMember]
        public decimal Amount { set; get; }

        /// <summary>
        /// 缴费登记ID
        /// </summary>
        [DataMember]
        public Guid PaymentRecordId { set; get; }
    }
}
