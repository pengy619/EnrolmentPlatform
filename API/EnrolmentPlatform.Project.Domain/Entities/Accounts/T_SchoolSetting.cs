using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_SchoolSetting")]
    [DataContract]
    public class T_SchoolSetting : Entity
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [DataMember]
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 学校ID
        /// </summary>
        [DataMember]
        public Guid SchoolId { get; set; }
    }
}
