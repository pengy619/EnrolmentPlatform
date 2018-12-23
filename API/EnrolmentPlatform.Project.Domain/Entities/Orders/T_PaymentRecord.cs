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
    /// 缴费登记信息
    /// </summary>
    [Serializable]
    [Table("T_PaymentRecord")]
    [DataContract]
    public class T_PaymentRecord:Entity
    {
        /// <summary>
        /// 缴费名称
        /// </summary>
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 缴费类型（1:普通缴费  2:尾款）
        /// </summary>
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 缴费金额
        /// </summary>
        [DataMember]
        public decimal TotalAmount { set; get; }

        /// <summary>
        /// 流水附件
        /// </summary>
        [DataMember]
        public string FilePath { set; get; }

        /// <summary>
        /// 支付发起方（1：机构，2：渠道）
        /// </summary>
        [DataMember]
        public int PaymentSource { set; get; }

        /// <summary>
        /// 支付发起ID（机构ID，学习中心ID）
        /// </summary>
        [DataMember]
        public Guid? PaymentSourceId { set; get; }

        /// <summary>
        /// 状态（1:待审核 2:已审核 3:审核拒绝）
        /// </summary>
        [DataMember]
        public int Status { set; get; }

        /// <summary>
        /// 审核人
        /// </summary>
        [DataMember]
        public string Auditor { set; get; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        [DataMember]
        public Guid? AuditorId { set; get; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [DataMember]
        public DateTime? AuditTime { set; get; }
    }
}
