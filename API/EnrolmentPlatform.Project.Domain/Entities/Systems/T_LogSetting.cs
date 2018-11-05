using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Table("T_LogSetting")]
    [Serializable]
    public class T_LogSetting : Entity
    {
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string BusinessName { get; set; }
        [DataMember]
        public Guid PrimaryKey { get; set; }
        [DataMember]
        public Guid ModuleKey { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public string OldContent { get; set; }
        [DataMember]
        public string NewContent { get; set; }
    }
}