using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_File")]
    [DataContract]
    public class T_File : Entity
    {
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
