using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_SchoolLevelMajor")]
    [DataContract]
    public class T_SchoolLevelMajor : Entity
    {
        /// <summary>
        /// 项目Id
        /// </summary>		
        [DataMember]
        public Guid ItemId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>		
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>		
        [DataMember]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>		
        [DataMember]
        public bool IsEnabled { get; set; }
    }
}