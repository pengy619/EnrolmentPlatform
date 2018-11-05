using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_Role")]
    [DataContract]
    public class T_Role : Entity
    {
        /// <summary>
        /// 角色名称
        /// </summary> 
        [DataMember]
        public string RoleName { get; set; }
        /// <summary>
        ///状态
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 企业编号
        /// </summary>
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Classify { get; set; }
    }
}
