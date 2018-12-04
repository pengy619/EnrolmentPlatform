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
        public List<LevelDto> LevelList { get; set; }
    }

    /// <summary>
    /// 层次Dto
    /// </summary>
    public class LevelDto
    {
        /// <summary>
        /// 层次Id
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// 专业集合
        /// </summary>
        public List<MajorDto> MajorList { get; set; }
    }

    /// <summary>
    /// 专业Id
    /// </summary>
    public class MajorDto
    {
        /// <summary>
        /// 专业Id
        /// </summary>
        public Guid MajorId { get; set; }
    }
}
