using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Product
{ 
    [Serializable]
    [DataContract]
    public class ProductForSpecialtyUnitDto
    {
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary> 
        [DataMember]
        public string UnitName { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary> 
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}
