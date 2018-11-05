using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    /// <summary>
    /// 系统类型枚举
    /// </summary>
    public enum SystemTypeEnum
    {
        /// <summary>
        /// 渠道中心
        /// </summary>
        [Description("渠道中心")]
        ChannelCenter = 1,

        /// <summary>
        /// 学习中心
        /// </summary>
        [Description("学习中心")]
        LearningCenter = 3,

        /// <summary>
        /// 培训机构
        /// </summary>
        [Description("培训机构")]
        TrainingInstitutions = 5
    }
}
