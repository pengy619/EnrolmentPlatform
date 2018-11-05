using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Finance
{
   public class SettlementCenterDto
    {
        /// <summary>
        /// 待结算金额
        /// </summary>
        public decimal PendingSettlePrice { get; set; }

        /// <summary>
        /// 结算金额 
        /// </summary>
        public decimal SettledPrice { get; set; }

        /// <summary>
        /// 结算周期
        /// </summary>
        public string SettlementPeriod { get; set; }
        /// <summary>
        /// 下次结算时间
        /// </summary>
        public string NextSettlementDate { get; set; }

        /// <summary>
        /// 上次结算时间
        /// </summary>
        public string LastSettlementDate { get; set; }

    }
}
