using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    /// <summary>
    /// 部门表 
    /// </summary>
    [Table("T_Department")]
    [Serializable]
    [DataContract]
    public class T_Department : Entity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int Sort { get; set; }
    }
}
