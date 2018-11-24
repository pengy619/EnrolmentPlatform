using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_ChargeStrategy")]
    [DataContract]
    public class T_ChargeStrategy : Entity
    {
        /// <summary>
        /// 学校专业Id
        /// </summary>		
        [DataMember]
        public Guid SchoolMajorId { get; set; }

        /// <summary>
        /// 策略名称
        /// </summary>		
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>		
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 机构费用
        /// </summary>		
        [DataMember]
        public decimal InstitutionCharge { get; set; }

        /// <summary>
        /// 中心费用
        /// </summary>		
        [DataMember]
        public decimal CenterCharge { get; set; }
    }
}