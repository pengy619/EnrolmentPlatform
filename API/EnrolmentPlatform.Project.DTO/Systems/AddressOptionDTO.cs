using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    public class AddressOptionDTO
    {
        [DataMember]
        public Guid Id { get; set; }
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
