using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_AccountVerification")]
    [DataContract]
    /// <summary>
    /// 用户核销表
    /// </summary>
    public class T_AccountVerification:Entity
    {
        /// <summary>
        /// 账号Id
        /// </summary>
        [DataMember]
        public Guid AccountId { get; set; }
        /// <summary>
        /// 景点或游乐项目Id 
        /// </summary>
        [DataMember]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 景点或游乐项目名称
        /// </summary>
        [DataMember]
        public string ResourceName { get; set; }
        /// <summary>
        /// 景点或游乐项目地址
        /// </summary>
        [DataMember]
        public string ResourceAddress { get; set; }
        /// <summary>
        /// 类型：【1：景点】【2：游乐项目】枚举【ScenicProjectClassifyEnum】
        /// </summary>
        [DataMember]
        public int Classify { get; set; }
        /// <summary>
        /// 企业Id
        /// </summary>
        [DataMember]
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}
