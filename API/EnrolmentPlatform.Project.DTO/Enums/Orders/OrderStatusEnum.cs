﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 订单状态（0：预报名, 1：提交 2：已退学，3：审核不通过，4：已报名，5：录取提交，6：录取拒绝，7：已录取）
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 预报名
        /// </summary>
        [Description("预报名")]
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

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        Reject = 3,

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

        /// <summary>
        /// 已初审
        /// </summary>
        [Description("已初审")]
        Audited = 6,

        /// <summary>
        /// 已录取
        /// </summary>
        [Description("已录取")]
        Join = 7,

        /// <summary>
        /// 已毕业
        /// </summary>
        [Description("已毕业")]
        Graduated = 8,

        /// <summary>
        /// 已休学
        /// </summary>
        [Description("已休学")]
        Suspended = 9
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

    /// <summary>
    /// 协助处理状态
    /// </summary>
    public enum AssistStatusEnum
    {
        /// <summary>
        /// 待处理
        /// </summary>
        [Description("待处理")]
        Approval = 1,

        /// <summary>
        /// 已处理
        /// </summary>
        [Description("已处理")]
        Approved = 2,
    }

    /// <summary>
    /// 订单审批状态（0：草稿，1：待审核，2：审核通过，3：审核失败）
    /// </summary>
    public enum OrderApprovalStatusEnum
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Init = 0,

        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Approval = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Approved = 2,

        /// <summary>
        /// 审核失败
        /// </summary>
        [Description("审核失败")]
        Faild = 3
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IDCardTypeEnum
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        Identity = 0,

        ///// <summary>
        ///// 港澳台居民证件
        ///// </summary>
        [Description("港澳台居民证件")]
        Resident = 1,

        /// <summary>
        /// 军人证
        /// </summary>
        [Description("军人证")]
        Soldier = 2,

        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        Passport = 3
    }
}
