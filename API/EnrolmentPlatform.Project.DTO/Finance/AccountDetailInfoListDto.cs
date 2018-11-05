using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Finance;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    /// <summary>
    ///  账户资金流水 请求类
    /// </summary>
    public class AccountDetailInfoListDto : GridDataRequest
    {
        /// <summary>
        /// 供应商（企业）ID
        /// </summary>
        public Guid? EnterpriseId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public TransactionTypeEnum? TransactionType { get; set; }
}


    /// <summary>
    /// 账户资产 
    /// </summary>
    public class AccountAssetsDto
    {
        /// <summary>
        /// 待结算金额
        /// </summary>
        public decimal PendingSettlePrice { get; set; }


        /// <summary>
        /// 账户余额/资产
        /// </summary>
        public decimal Assets { get; set; }

    }
}
