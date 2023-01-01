using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Basics
{
    /// <summary>
    /// 学校配置Dto
    /// </summary>
    public class SchoolConfigDto
    {
        /// <summary>
        /// 学校Id
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次集合
        /// </summary>
        public List<LevelMajorDto> LevelMajorList { get; set; }
    }

    /// <summary>
    /// 层次Dto
    /// </summary>
    public class LevelMajorDto
    {
        /// <summary>
        /// 层次Id
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// 专业Id
        /// </summary>
        public Guid MajorId { get; set; }
    }

    /// <summary>
    /// 学校证件配置Dto
    /// </summary>
    public class SchoolImageConfigDto
    {
        /// <summary>
        /// 学校ID
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 必须上传的证件
        /// </summary> 
        public List<SelectData> ImageTypeList { get; set; }
    }

    public class SelectData
    {
        public string value { get; set; }

        public string title { get; set; }
    }
}
