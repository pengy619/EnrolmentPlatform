using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Basics
{
    /// <summary>
    /// 收费策略Dto
    /// </summary>
    public class ChargeStrategyDto
    {
        /// <summary>
        /// Id
        /// </summary>		
        public Guid Id { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次ID
        /// </summary>
        public Guid LevelId { set; get; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public Guid MajorId { set; get; }

        /// <summary>
        /// 策略名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		
        public DateTime StartDate { get; set; }

        public string StartDateStr
        {
            get { return StartDate.ToString("yyyy-MM-dd"); }
        }

        /// <summary>
        /// 截止时间
        /// </summary>		
        public DateTime EndDate { get; set; }

        public string EndDateStr
        {
            get { return EndDate.ToString("yyyy-MM-dd"); }
        }

        /// <summary>
        /// 机构费用
        /// </summary>		
        public decimal InstitutionCharge { get; set; }

        /// <summary>
        /// 中心费用
        /// </summary>		
        public decimal CenterCharge { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreatorUserId { get; set; }
        
        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }

    /// <summary>
    /// 收费策略查询Dto
    /// </summary>
    public class ChargeStrategySearchDto : GridDataRequest
    {
        /// <summary>
        /// 学校Id
        /// </summary>		
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次ID
        /// </summary>
        public Guid LevelId { set; get; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public Guid MajorId { set; get; }
    }
}
