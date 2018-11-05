using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Product
{
    [Serializable]
    [DataContract]
    public class ProductForSpecialtyVarietiesDto
    {
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 产品分类ID
        /// </summary> 
        [DataMember]
        public Guid ProductCategoryId { get; set; }
        /// <summary>
        /// 农产品名称ID
        /// </summary> 
        [DataMember]
        public Guid SpecialtyNameId { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary> 
        [DataMember]
        public string VarietiesName { get; set; }
        /// <summary>
        /// 状态
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
    }


    [Serializable]
    [DataContract]
    public class ProductForSpecialtyVarietiesForAdminDto
    {
        /// <summary>
        /// 品种名称
        /// </summary> 
        [DataMember]
        public Guid VarietiesId { get; set; }
        /// <summary>
        /// 产品分类ID
        /// </summary> 
        [DataMember]
        public Guid ProductCategoryId { get; set; }
        /// <summary>
        /// 农产品名称ID
        /// </summary> 
        [DataMember]
        public Guid SpecialtyNameId { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary> 
        [DataMember]
        public string VarietiesName { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 品种图片
        /// </summary> 
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; }
        /// <summary>
        /// 规格参数
        /// </summary> 
        [DataMember]
        public List<ListForProductForSpecialtyFormatNameForSupplierDto> ListForProductForSpecialtyFormatNameForSupplierDto { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ProductSpecialtyVarietiesForListForAdminDto
    {
        /// <summary>
        /// 品种
        /// </summary> 
        [DataMember]
        public Guid VarietiesId { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary> 
        [DataMember]
        public string ProductCategory { get; set; }
        /// <summary>
        /// 农产品名称ID
        /// </summary> 
        [DataMember]
        public string SpecialtyName { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary> 
        [DataMember]
        public string VarietiesName { get; set; }
        /// <summary>
        /// 时间
        /// </summary> 
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusBaseEnum)Status);
            }
        }
    }

    [Serializable]
    [DataContract]
    public class ProductSpecialtyVarietiesForQueryForAdminDto : GridDataRequest
    {
        /// <summary>
        /// 产品分类ID
        /// </summary> 
        [DataMember]
        public Guid ProductCategoryId { get; set; }
        /// <summary>
        /// 农产品名称
        /// </summary> 
        [DataMember]
        public string SpecialtyName { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary> 
        [DataMember]
        public string VarietiesName { get; set; }
        /// <summary>
        /// 状态
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
    }
}
