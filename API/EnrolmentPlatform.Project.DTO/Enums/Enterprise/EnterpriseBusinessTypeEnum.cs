using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Enterprise
{
    /// <summary>
    /// 企业业务类型
    /// </summary>
    public enum EnterpriseBusinessTypeEnum
    {
        /// <summary>
        /// 农户
        /// </summary>
        [Description("农户")]
        Farmer = 1,

        /// <summary>
        /// 商户
        /// </summary>
        [Description("商户")]
        CommercialTenant = 2
    }

    /// <summary>
    /// 供应商类型 
    /// </summary>
    public enum SupplierTypeEnum
    {
        /// <summary>
        /// 自营供应商
        /// </summary>
        [Description("自营供应商")]
        Self = 1,

        /// <summary>
        /// 外部供应商
        /// </summary>
        [Description("外部供应商")]
        External = 2,

    }
}
