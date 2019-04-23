using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    public class FileDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 外键ID
        /// </summary> 
        public Guid ForeignKeyId { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary> 
        public string FilePath { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary> 
        public string FileName { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }
}
