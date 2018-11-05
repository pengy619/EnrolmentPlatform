using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Product
{  
    /// <summary>
    /// 套餐列表搜索条件参数
    /// </summary>
    public class SearchParamForCateringPackageDto : GridDataRequest
    { 
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }  
        /// <summary>
        /// 套餐名称
        /// </summary>
        public string ProductName { get; set; } 
        /// <summary>
        /// 供应商名
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 不包含的状态
        /// </summary>
        public int StatusForNo { get; set; } 
    }


    /// <summary>
    /// 套餐列表信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ListForProductForCateringPackageDataDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid ProductId { get; set; }  
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单已售
        /// </summary>
        [DataMember]
        public int OrderCount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ProductStatusEnum)Status);
            }
        } 
    }

    /// <summary>
    /// 套餐信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductForCateringPackageDataDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 套餐描叙
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// 用餐人数
        /// </summary>
        [DataMember]
        public int UsePeople { get; set; }
        /// <summary>
        /// 最低市场价
        /// </summary> 
        [DataMember]
        public decimal MinMarkPrice { get; set; }
        /// <summary>
        /// 最低分销价
        /// </summary> 
        [DataMember]
        public decimal MinDistributionPrice { get; set; }
        /// <summary>
        /// 是否需要预约
        /// </summary>
        [DataMember]
        public bool IsSubscribe { get; set; }
        /// <summary>
        /// 退票规则
        /// </summary>
        [DataMember]
        public int RefundRule { get; set; } 
        /// <summary>
        /// 开始有效期
        /// </summary>
        [DataMember]
        public DateTime StartExpiryDate { get; set; }
        /// <summary>
        /// 结束有效期
        /// </summary>
        [DataMember]
        public DateTime EndExpiryDate { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        [DataMember]
        public string UseTime { get; set; }
        /// <summary>
        /// 使用规则
        /// </summary>
        [DataMember]
        public string UseRule { get; set; }
        /// <summary>
        /// 提交审核时间
        /// </summary>
        [DataMember]
        public DateTime ApplyOnSaleTime { get; set; } 
        /// <summary>
        /// 最大使用人数
        /// </summary>
        [DataMember]
        public int MaxUsePeople { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ProductStatusEnum)Status);
            }
        }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; }


        [DataMember]
        public Guid SupplierId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; } 
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public string Reason { get; set; }  
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public string ShopName { get; set; }
    }

    /// <summary>
    /// 套餐信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductForCateringPackageDetailDataDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid ProductId { get; set; }   
        [DataMember]
        public Guid SupplierId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ProductStatusEnum)Status);
            }
        }
        /// <summary>
        /// 套餐详情
        /// </summary>
        [DataMember]
        public string PackageDetailInfo { get; set; }
        /// <summary>
        /// 套餐项目
        /// </summary>
        [DataMember]
        public List<ProductForCateringPackageProjectDto> ProductForCateringPackageProjectDto { get; set; }
    }

    /// <summary>
    /// 套餐信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductForCateringPackageAllDto
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        [DataMember]
        public ProductForCateringPackageDataDto ProductForCateringPackageDataDto { get; set; }
        /// <summary>
        /// 详细信息
        /// </summary>
        [DataMember]
        public ProductForCateringPackageDetailDataDto ProductForCateringPackageDetailDataDto { get; set; }
    }

    /// <summary>
    /// 套餐信息-项目
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductForCateringPackageProjectDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [DataMember]
        public string PeojectName { get; set; }
        /// <summary>
        /// 详细信息
        /// </summary>
        [DataMember]
        public List<ProductForCateringPackageProjectConDto> ProductForCateringPackageProjectConDto { get; set; }
    }

    /// <summary>
    /// 套餐信息-项目内容
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductForCateringPackageProjectConDto
    {
        /// <summary>
        /// 菜（食物）Id
        /// </summary>
        [DataMember]
        public Guid FoodId { get; set; }
        /// <summary>
        /// 菜名
        /// </summary>
        [DataMember]
        public string FoodName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        [DataMember]
        public decimal SubTotalPrice { get; set; }
    }

    /// <summary>
    /// 套餐列表搜索条件参数
    /// </summary>
    public class SearchParamForFoodDto : GridDataRequest
    {
        /// <summary>
        /// 供应商
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; } 
    }
}
