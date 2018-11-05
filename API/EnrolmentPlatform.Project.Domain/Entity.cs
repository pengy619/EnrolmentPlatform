using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain
{
    [Serializable]
    [DataContract]
    public class Entity
    {
        [Key]
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
  
    }
}
