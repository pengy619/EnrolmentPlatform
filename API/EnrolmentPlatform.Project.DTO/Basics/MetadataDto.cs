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

        /// <summary>
        /// 是否启用
        /// </summary> 
        public bool IsEnable { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>		
        public DateTime? EndDate { get; set; }
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
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
    }
}