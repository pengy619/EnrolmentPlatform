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
    /// 缴费登记ID
    /// </summary>
    [Serializable]
    [Table("T_PaymentRecord")]
    [DataContract]
    public class T_PaymentRecord:Entity
    {
        /// <summary>
        /// 缴费名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 缴费类型（1:普通缴费  2:尾款）
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 缴费金额
        /// </summary>
        public decimal TotalAmount { set; get; }

        /// <summary>
        /// 流水附件
        /// </summary>
        public string FilePath { set; get; }

        /// <summary>
        /// 支付发起方（1：机构，2：渠道）
        /// </summary>
        public int PaymentSource { set; get; }

        /// <summary>
        /// 支付发起ID
        /// </summary>
        public Guid? PaymentSourceId { set; get; }

        /// <summary>
        /// 状态（1:待审核 2:已审核 3:审核拒绝）
        /// </summary>
        public int Status { set; get; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { set; get; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid AuditorId { set; get; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime { set; get; }
    }
}
