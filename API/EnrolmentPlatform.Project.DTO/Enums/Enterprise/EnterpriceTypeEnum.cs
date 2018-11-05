using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Enterprise
{
    /// <summary>
    /// 企业类型
    /// </summary>
    public enum EnterpriceTypeEnum
    {
        /// <summary>
        /// 景区运营端用户
        /// </summary>
        [Description("景区运营端")]
        Admin = 1,

        /// <summary>
        /// 用户端
        /// </summary>
        [Description("用户端")]
        Customer = 2,

        /// <summary>
        /// 供应商
        /// </summary>
        [Description("供应商")]
        Supplier = 3,

        /// <summary>
        /// 分销商
        /// </summary>
        [Description("分销商")]
        Distributor = 4,
        /// <summary>
        /// 景区服务中心
        /// </summary>
        [Description("景区服务中心")]
        ScenicServiceCenter = 5
    }
}
