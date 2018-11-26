using System;
using EnrolmentPlatform.Project.DTO.Enums.Basics;

namespace EnrolmentPlatform.Project.DTO.Basics
{
    /// <summary>
    /// 元数据Dto
    /// </summary>
    public class MetadataDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MetadataTypeEnum Type { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid CreatorUserId { set; get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string CreatorAccount { set; get; }
    }

    /// <summary>
    /// 元数据查询Dto
    /// </summary>
    public class MetadataSearchDto : GridDataRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        public MetadataTypeEnum Type { get; set; }
    }
}