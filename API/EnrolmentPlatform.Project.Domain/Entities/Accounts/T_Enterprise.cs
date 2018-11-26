using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_Enterprise")]
    [DataContract]
    public class T_Enterprise : Entity
    {
        /// <summary>
        /// 企业名称
        /// </summary> 
        [DataMember]
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary> 
        [DataMember]
        public string Contact { get; set; }
        /// <summary>
        /// 手机号
        /// </summary> 
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// 类型【3：学习中心】【5：培训机构】
        /// </summary> 
        [DataMember]
        public int Classify { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 状态【1：未启用】【2：启用】
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
    }
}
