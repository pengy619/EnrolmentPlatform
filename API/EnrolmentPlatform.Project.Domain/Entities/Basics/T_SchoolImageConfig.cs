using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_SchoolImageConfig")]
    [DataContract]
    public class T_SchoolImageConfig : Entity
    {
        /// <summary>
        /// 学校ID
        /// </summary>
        [DataMember]
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [DataMember]
        public int ImageType { get; set; }
    }
}
