using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisdomScenic.Project.DTO.Enums.Enterprise
{
    /// <summary>
    /// 企业状态
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 2
    }
}
