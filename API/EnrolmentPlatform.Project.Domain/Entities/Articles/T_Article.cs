using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_Article")]
    [DataContract]
    public class T_Article : Entity
    {
        /// <summary>
        /// 标题
        /// </summary> 
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary> 
        [DataMember]
        public Guid ClassifyId { get; set; }

        /// <summary>
        /// 内容
        /// </summary> 
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 状态
        /// </summary> 
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        public string Abstract { get; set; }

        /// <summary>
        /// 文件地址
        /// </summary>
        public string FilePath { get; set; }
    }
}
