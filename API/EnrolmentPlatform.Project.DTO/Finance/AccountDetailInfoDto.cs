using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Finance;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    public class AccountDetailInfoDto
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public TransactionTypeEnum TransactionType { get; set; }

        public string TransactionTypeName
        {
            get { return EnumDescriptionHelper.GetDescription(TransactionType); }
        }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TranscationNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 交易前金额
        /// </summary>
        public decimal TranscationBeforeAmount { get; set; }

        /// <summary>
        ///   交易后金额
        /// </summary>
        public decimal TranscationAfterAmount { get; set; }

    }
}
