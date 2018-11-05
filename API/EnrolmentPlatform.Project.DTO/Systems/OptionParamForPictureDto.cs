using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 图片操作类
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForPictureDto
    {
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 外键ID
        /// </summary> 
        [DataMember]
        public Guid ForeignKeyId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary> 
        [DataMember]
        public string FilePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary> 
        [DataMember]
        public string FileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary> 
        [DataMember]
        public int FileClassify { get; set; }
        /// <summary>
        /// 外键类型
        /// </summary> 
        [DataMember]
        public int ForeignKeyClassify { get; set; }

        /// <summary>
        /// 文件外键业务类型
        /// </summary>
        public int FileBusinessType { set; get; }

        /// <summary>
        /// 是否封面图
        /// </summary> 
        [DataMember]
        public bool Iscover { get; set; }
        /// <summary>
        /// 是否轮播
        /// </summary> 
        [DataMember]
        public bool IsFocus { get; set; }
    }
}
