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
    public class ProductCategoriesDto
    {
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary> 
        [DataMember]
        public int Classify { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        [DataMember]
        public string Name { get; set; } 
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}
