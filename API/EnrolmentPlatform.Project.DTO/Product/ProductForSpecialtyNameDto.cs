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
    public class ProductForSpecialtyNameDto
    {
        [DataMember] 
        public Guid Id { get; set; }
        /// <summary>
        /// 农产品名称
        /// </summary> 
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 农产品分类
        /// </summary> 
        [DataMember]
        public Guid ProductCategoriesId { get; set; }

        /// <summary>
        /// 农产品品种
        /// </summary> 
        [DataMember]
        public List<ProductForSpecialtyVarietiesDto> SpecialtyVarieties { get; set; }
    }
}
