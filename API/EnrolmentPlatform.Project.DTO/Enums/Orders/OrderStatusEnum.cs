using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 订单状态（0：草稿, 1：提交 2：已退学，3：已拒绝，4：已报名，5：录取提交，6：录取拒绝，7：已录取）
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Init = 0,

        ///// <summary>
        ///// 提交
        ///// </summary>
        //[Description("提交")]
        //Submit = 1,

        /// <summary>
        /// 已退学
        /// </summary>
        [Description("已退学")]
        LeaveSchool = 2,

        ///// <summary>
        ///// 已拒绝
        ///// </summary>
        //[Description("已拒绝")]
        //Reject = 3,

        /// <summary>
        /// 已报名
        /// </summary>
        [Description("已报名")]
        Enroll = 4,

        /// <summary>
        /// 已报送中心
        /// </summary>
        [Description("已报送")]
        ToLearningCenter = 5,

        ///// <summary>
        ///// 录取拒绝
        ///// </summary>
        //[Description("录取拒绝")]
        //JoinReject = 6,

        /// <summary>
        /// 已录取
        /// </summary>
        [Description("已录取")]
        Join = 7
    }

    /// <summary>
    /// 付款单状态（1:待审核 2:已审核 3:审核拒绝）
    /// </summary>
    public enum PaymentStatusEnum
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Submit = 1,

        ///// <summary>
        ///// 已审核
        ///// </summary>
        [Description("已审核")]
        Approved = 2,

        /// <summary>
        /// 审核拒绝
        /// </summary>
        [Description("审核拒绝")]
        Reject = 3,
    }

    /// <summary>
    /// 付款单类型（1:普通缴费  2:尾款）
    /// </summary>
    public enum PaymentTypeEnum
    {
        /// <summary>
        /// 普通缴费
        /// </summary>
        [Description("普通缴费")]
        Normal = 1,

        /// <summary>
        /// 尾款
        /// </summary>
        [Description("尾款")]
        EndPayment = 2,
    }
}
