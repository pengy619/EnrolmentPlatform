using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    /// <summary>
    ///  银行卡解绑  
    /// </summary>
    public class UnbindingBankCardDto
    {
        public Guid Id { get; set; }

        public Guid LastModifyUserId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

    }
}
