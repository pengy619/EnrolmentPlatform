using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Product
{
    /// <summary>
    /// 店铺状态 枚举
    /// </summary>
    public enum StatusForShopEnum
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Init =0,

        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Checking = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("已启用")]
        Enable = 2,

        /// <summary>
        /// 审核拒绝
        /// </summary>
        [Description("审核拒绝")]
        CheckNo = 3,

        /// <summary>
        /// 已禁用
        /// </summary>
        [Description("已禁用")]
        DisEnable = 4
    }

}
