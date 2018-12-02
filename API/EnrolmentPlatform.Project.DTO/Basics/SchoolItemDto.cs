using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Basics
{
    /// <summary>
    /// 学校项目Dto
    /// </summary>
    public class SchoolItemDto
    {
        /// <summary>
        /// Id
        /// </summary>		
        public Guid Id { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>		
        public Guid ItemId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
    }
}
