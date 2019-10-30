using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums
{
    /// <summary>
    /// 自定义字段类型枚举
    /// </summary>
    public enum CustomerFieldTypeEnum
    {
        /// <summary>
        /// 文本框
        /// </summary>
        [Description("文本框")]
        Text = 1,

        /// <summary>
        /// 下拉单选框
        /// </summary>
        [Description("下拉单选框")]
        Select = 2,
    }
}
