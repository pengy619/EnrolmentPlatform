using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 核销状态
    /// </summary>
    public enum TicketVerificationStatusEnum
    {
        /// <summary>
        /// 待核销
        /// </summary>
        [Description("待核销")]
        NoVerification = 1,
        /// <summary>
        /// 已锁定
        /// </summary>
        [Description("已锁定")]
        Locked = 2,
        /// <summary>
        /// 已核销
        /// </summary>
        [Description("已核销")]
        Verificated = 3,
        /// <summary>
        /// 已失效
        /// </summary>
        [Description("已失效")]
        Invalid = 4,
    }
    /// <summary>
    /// 景点项目类型
    /// </summary>
    public enum ScenicProjectClassifyEnum
    {
        /// <summary>
        /// 景点
        /// </summary>
        [Description("景点")]
        ScenicSpot = 1,
        /// <summary>
        /// 游乐项目
        /// </summary>
        [Description("游乐项目")]
        AmusementProject = 2,
    }
    /// <summary>
    /// 核销方式
    /// </summary>
    public enum VerificationModeEnum
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        ID = 1,
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("供应商后台")]
        SupplierBackstage = 2,
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("游客服务中心")]
        TouristServiceCenter = 3,
    }
    /// <summary>
    /// 核销状态
    /// </summary>
    public enum CateringVerificationStatusEnum
    {
        /// <summary>
        /// 待核销
        /// </summary>
        [Description("待核销")]
        NoVerification = 1,
        /// <summary>
        /// 已锁定
        /// </summary>
        [Description("已锁定")]
        Locked = 2,
        /// <summary>
        /// 已核销
        /// </summary>
        [Description("已核销")]
        Verificated = 3,
        /// <summary>
        /// 已失效
        /// </summary>
        [Description("已失效")]
        Invalid = 4,
    }
}
