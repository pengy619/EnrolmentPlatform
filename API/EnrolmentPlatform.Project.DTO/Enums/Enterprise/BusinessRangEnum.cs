using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Enterprise
{
    /// <summary>
    /// 业务范围枚举
    /// </summary>
    public enum BusinessRangEnum
    {
        /// <summary>
        /// 游乐项目
        /// </summary>
        [Description("游乐项目")]
        PlayProject = 1,

        /// <summary>
        /// 酒店/民宿
        /// </summary>
        [Description("酒店/民宿")]
        Hotel = 2,

        /// <summary>
        /// 农产品
        /// </summary>
        [Description("农产品")]
        FarmProduct = 3,

        /// <summary>
        /// 土特产
        /// </summary>
        [Description("土特产")]
        LocalSpecialty=4
    }
}
