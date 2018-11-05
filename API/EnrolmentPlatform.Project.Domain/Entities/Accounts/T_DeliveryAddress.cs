using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_DeliveryAddress")]
    [DataContract]
    public class T_DeliveryAddress : Entity
    {
        /// <summary>
        /// 用户ID
        /// </summary> 
        [DataMember]
        public Guid AccountId { get; set; }

        /// <summary>
        /// 收货人
        /// </summary> 
        [DataMember]
        public string Consignee { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary> 
        [DataMember]
        public string ContactPhone { get; set; }

        /// <summary>
        /// 具体地址
        /// </summary> 
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// 完整地址名称
        /// </summary> 
        [DataMember]
        public string FullAddressName { get; set; }

        /// <summary>
        /// 地址ID
        /// </summary> 
        [DataMember]
        public Guid AddressId { get; set; }

        /// <summary>
        /// 默认地址
        /// </summary> 
        [DataMember]
        public bool IsDefault { get; set; }
    }
}
