using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    /// <summary>
    /// 广告位置枚举
    /// </summary>
    public enum BannerPositionEnum
    {
        [Description("预售")]
        PreSale = 1,
        [Description("现货")]
        SoldNow = 2,
        [Description("右侧上方")]
        RightUpper = 3,
        [Description("右侧下方")]
        RightBelow = 4
    }
}
