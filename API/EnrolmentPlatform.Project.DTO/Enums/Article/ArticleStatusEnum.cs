using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums
{
    /// <summary>
    /// 文章状态枚举
    /// </summary>
    public enum ArticleStatusEnum
    {
        [Description("草稿")]
        Draft = 0,
        [Description("已发布")]
        Publish = 1
    }
}
