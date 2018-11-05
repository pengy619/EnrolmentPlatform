using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    /// <summary>
    /// 职位表
    /// </summary>
    [Table("T_Job")]
    [Serializable]
    [DataContract]
    public class T_Job : Entity
    {
        /// <summary>
        /// 职位名称 
        /// </summary>
        [DataMember]
        public string JobName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int Sort { get; set; }

    }
}
