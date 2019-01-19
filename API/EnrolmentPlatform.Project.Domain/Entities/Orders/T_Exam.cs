using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_Exam")]
    [DataContract]
    public class T_Exam : Entity
    {
        /// <summary>
        /// 考试名称
        /// </summary>		
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 学院中心
        /// </summary>
        [DataMember]
        public Guid LearningCenterId { set; get; }
    }
}