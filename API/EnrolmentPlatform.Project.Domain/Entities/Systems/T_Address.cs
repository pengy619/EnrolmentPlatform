using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_Address")]
    [DataContract]
    public class T_Address : Entity
    {
        [DataMember]
        public string ChinaName { get; set; }
        [DataMember]
        public string EnglishName { get; set; }
        [DataMember]
        public string Pinyin { get; set; }
        [DataMember]
        public string ShortPinyin { get; set; }
        [DataMember]
        public int Classify { get; set; }
        [DataMember]
        public string ChinaRoute { get; set; }
        [DataMember]
        public string PinyinRoute { get; set; }
        [DataMember]
        public bool IsMunicipality { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string AreaCode { get; set; }
        [DataMember]
        public Guid ParentId { get; set; }
    }
}
