using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO
{
    /// <summary>
    /// 文章栏目Dto
    /// </summary>
    public class ArticleCategoryDto
    {
        /// <summary>
        /// 栏目Id
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 创建者账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }
}
