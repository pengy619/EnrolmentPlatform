using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    /// <summary>
    /// 广告类型枚举
    /// </summary>
    public enum BannerClassifyEnum
    {
        [Description("首页")]
        HomePage = 0,
        [Description("农产品列表")]
        SpecialtyList = 1,
        [Description("农产品详情")]
        SpecialtyDetail = 2
    }
}
