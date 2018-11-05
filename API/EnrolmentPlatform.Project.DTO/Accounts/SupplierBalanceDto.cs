using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Accounts
{
    /// <summary>
    ///  供应商账户余额 Dto
    /// </summary>
    public class SupplierBalanceDto
    {
        /// <summary>
        ///  ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  供应商名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 是否设置提现密码
        /// </summary>
        public bool IsSetPassword { get; set; }
    }
}
