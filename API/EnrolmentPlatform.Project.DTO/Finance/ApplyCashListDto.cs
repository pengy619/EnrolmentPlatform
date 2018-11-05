using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Finance;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    /// <summary>
    /// 提现管理 列表  请求类
    /// </summary>
    public class ApplyCashListDto : GridDataRequest
    {
        /// <summary>
        /// 企业（供应商）ID
        /// </summary>
        public Guid? EnterpriseId { get; set; }

        /// <summary>
        ///  审核状态
        /// </summary>
        public CashApplyStatusEnum? Status { get; set; }

        /// <summary>
        /// 开始时间 
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间  
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        ///  企业名称（供应商名称）
        /// </summary>
        public string EnterpriseName { get; set; }

    }
}
