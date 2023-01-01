using EnrolmentPlatform.Project.DTO.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Accounts
{
    /// <summary>
    /// 招生机构学校配置DTO
    /// </summary>
    public class SchoolSettingDto
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 不可报读的学校
        /// </summary> 
        public List<SelectData> SchoolList { get; set; }
    }
}
