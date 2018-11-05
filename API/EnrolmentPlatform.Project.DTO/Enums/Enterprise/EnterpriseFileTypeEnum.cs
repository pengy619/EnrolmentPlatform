using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Enterprise
{
    /// <summary>
    /// 企业文件类型枚举
    /// </summary>
    public enum EnterpriseFileTypeEnum
    {
        /// <summary>
        /// 营业执照
        /// </summary>
        [Description("营业执照")]
        BusinessLicense = 1,

        /// <summary>
        /// 身份证正面
        /// </summary>
        [Description("身份证正面")]
        IDCardUpwards = 2,

        /// <summary>
        /// 身份证反面
        /// </summary>
        [Description("身份证反面")]
        IDCardReverse = 3,
    }
}
