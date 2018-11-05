using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Finance
{
    /// <summary>
    /// 银行卡账户类型 枚举
    /// </summary>
    public enum BankCardTypeEnum
    {
        /// <summary>
        /// 企业银行卡
        /// </summary>
        [Description("企业银行卡")]
        Enterprise=0,

        /// <summary>
        /// 个人银行卡
        /// </summary>
        [Description("个人银行卡")]
        Person = 1
    }
}
