using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Orders
{
    [Serializable]
    [Table("T_PaymentInfo")]
    [DataContract]
    public class T_PaymentInfo : Entity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 缴费金额
        /// </summary>
        public decimal Amount { set; get; }

        /// <summary>
        /// 缴费登记ID
        /// </summary>
        public Guid PaymentRecordId { set; get; }
    }
}
