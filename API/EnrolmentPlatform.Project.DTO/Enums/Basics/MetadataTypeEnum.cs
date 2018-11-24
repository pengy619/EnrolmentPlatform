using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Basics
{
    /// <summary>
    /// 基础数据类型
    /// </summary>
    public enum MetadataTypeEnum
    {
        /// <summary>
        /// 学校
        /// </summary>
        [Description("学校")]
        School = 1,

        /// <summary>
        /// 层次
        /// </summary>
        [Description("层次")]
        Level = 2,

        /// <summary>
        /// 专业
        /// </summary>
        [Description("专业")]
        Major = 3,

        /// <summary>
        /// 批次
        /// </summary>
        [Description("批次")]
        Batch = 3
    }
}
