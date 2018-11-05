using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.DTO.Product
{
    [Serializable]
    [DataContract]
    public class ListForProductForSpecialtyFormatDataForSupplierDto
    {
        /// <summary>
        /// 品种id
        /// </summary> 
        [DataMember]
        public Guid VarietiesId { get; set; } 
        /// <summary>
        /// 品种图片
        /// </summary> 
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; }
        /// <summary>
        /// 参数
        /// </summary> 
        [DataMember]
        public List<ListForProductForSpecialtyFormatNameForSupplierDto> ListForProductForSpecialtyFormatNameForSupplierDto { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ListForProductForSpecialtyFormatNameForSupplierDto
    { 
        /// <summary>
        /// 规格名称
        /// </summary> 
        [DataMember]
        public string FormatName { get; set; }

        /// <summary>
        /// 参数
        /// </summary> 
        [DataMember]
        public List<string> Param { get; set; }
    } 
}
