using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Article
{
    /// <summary>
    /// 发布对象枚举
    /// </summary>
    public enum PublicObjectEnum
    {
        /// <summary>
        /// B2C网站
        /// </summary>
        [Description("B2C网站")]
        B2C = 1,
        /// <summary>
        /// 供应商
        /// </summary>
        [Description("供应商")]
        Supplier = 2,
        /// <summary>
        /// 游客服务中心
        /// </summary>
        [Description("游客服务中心")]
        ServiceCenter = 3,
        /// <summary>
        /// 总部
        /// </summary>
        [Description("总部")]
        Admin = 4,
        /// <summary>
        /// H5
        /// </summary>
        [Description("H5")]
        H5 = 5
    }
}
