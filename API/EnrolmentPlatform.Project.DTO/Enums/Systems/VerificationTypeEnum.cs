using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    /// <summary>
    /// 资源核销枚举
    /// </summary>
    public enum VerificationTypeEnum
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
        AmusementProject = 2
    }
}
