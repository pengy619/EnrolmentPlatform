using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_RolePermissionsRelation")]
    [DataContract]
    public class T_RolePermissionsRelation
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public Guid Id { set; get; }

        /// <summary>
        /// 角色编号
        /// </summary> 
        [DataMember]
        public Guid RoleId { get; set; }
        /// <summary>
        /// 权限编号
        /// </summary> 
        [DataMember]
        public Guid PermissionsId { get; set; }
    }
}
