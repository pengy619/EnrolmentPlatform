using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 付款单
    /// </summary>
    public class PaymentRecordDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 缴费名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 缴费类型（1:普通缴费  2:尾款）
        /// </summary>
        public PaymentTypeEnum Type { set; get; }

        /// <summary>
        /// 缴费金额
        /// </summary>
        public decimal TotalAmount { set; get; }

        /// <summary>
        /// 缴费单价
        /// </summary>
        public decimal UnitAmount { set; get; }

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
        public PaymentStatusEnum Status { set; get; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { set; get; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? AuditorId { set; get; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { set; get; }

        /// <summary>
        /// 付款单订单信息
        /// </summary>
        public List<PaymentOrderInfo> OrderList { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 创建用户Id
        /// </summary>
        public Guid UserId { set; get; }
    }

    /// <summary>
    /// 付款单信息
    /// </summary>
    public class PaymentOrderInfo
    {
        /// <summary>
        /// 订单金额
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 报名批次
        /// </summary>
        public string BatchName { set; get; }

        /// <summary>
        /// 报考学校
        /// </summary>
        public string SchoolName { set; get; }

        /// <summary>
        /// 报读层次
        /// </summary>
        public string LevelName { set; get; }

        /// <summary>
        /// 报读专业
        /// </summary>
        public string MajorName { set; get; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { set; get; }
    }
}
