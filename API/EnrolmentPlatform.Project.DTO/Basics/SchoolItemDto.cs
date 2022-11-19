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

        /// <summary>
        /// 父级Id
        /// </summary>		
        public Guid ParentId { get; set; }
    }

    /// <summary>
    /// 学校列表DTO
    /// </summary>
    public class SchoolItemListDto
    {
        /// <summary>
        /// 学习形式
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 层次ID
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// 层次名称
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public Guid MajorId { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { get; set; }
    }

    /// <summary>
    /// 学校列表查询DTO
    /// </summary>
    public class SchoolItemListReqDto : GridDataRequest
    {
        /// <summary>
        /// 招生机构ID
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 层次名称
        /// </summary>
        public string LevelName { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { get; set; }
    }
}
