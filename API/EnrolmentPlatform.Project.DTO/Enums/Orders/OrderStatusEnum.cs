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
        [Description("学校")]
        Init =0,

        /// <summary>
        /// 提交
        /// </summary>
        [Description("提交")]
        Submit = 1,

        /// <summary>
        /// 已退学
        /// </summary>
        [Description("已退学")]
        LeaveSchool = 2,

        /// <summary>
        /// 已拒绝
        /// </summary>
        [Description("已拒绝")]
        Reject = 3,

        /// <summary>
        /// 已报名
        /// </summary>
        [Description("已报名")]
        Enroll = 4,

        /// <summary>
        /// 已报送中心
        /// </summary>
        [Description("已报送中心")]
        ToLearningCenter = 5,

        /// <summary>
        /// 录取拒绝
        /// </summary>
        [Description("录取拒绝")]
        JoinReject = 6,

        /// <summary>
        /// 已录取
        /// </summary>
        [Description("已录取")]
        Join = 7
    }
}
