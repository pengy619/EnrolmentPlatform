using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 农产品列表操作条件参数
    /// </summary>
    public class OptionParamForSpecialtyDto
    {
        /// <summary>
        /// 产品id数组
        /// </summary>
        public List<Guid> ProductIds { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        public Guid ModifyUserId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 拒绝理由
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public int ProductClassify { get; set; }
    }

    /// <summary>
    /// 农产品规格添加参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForSpecialtyForFormatDto
    {
        /// <summary>
        /// 品种id
        /// </summary>
        [DataMember]
        public Guid VarietiesId { get; set; }
        /// <summary>
        /// 规格名称
        /// </summary>
        [DataMember]
        public string FormatName { get; set; }
        /// <summary>
        /// 规格参数
        /// </summary>
        [DataMember]
        public string FormatParms { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        [DataMember]
        public Guid CreatorUserId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }
    }


    /// <summary>
    /// 农产品查询详情参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class ParamForSpecialtyForDetailDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }
    }


    /// <summary>
    /// 农产品添加参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForSpecialtyForDataDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 品种id
        /// </summary>
        [DataMember]
        public Guid VarietiesId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [DataMember]
        public string ProductNumber { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [DataMember]
        public Guid ProductCategoriesId { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 下架时间
        /// </summary>
        [DataMember]
        public DateTime OffSaleTime { get; set; }
        /// <summary>
        /// 产品说明
        /// </summary>
        [DataMember]
        public string SpecialtyExplain { get; set; }
        /// <summary>
        /// 销售模式
        /// </summary>
        [DataMember]
        public int SalesModel { get; set; }
        /// <summary>
        /// 销售单位
        /// </summary>
        [DataMember]
        public string SaleslUnit { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        [DataMember]
        public int Inventory { get; set; }
        /// <summary>
        /// 已卖库存数
        /// </summary> 
        [DataMember]
        public int SoldInventory { get; set; }
        /// <summary>
        /// 定金比例
        /// </summary>
        [DataMember]
        public decimal DepositRatio { get; set; }
        /// <summary>
        /// 最小购买数量
        /// </summary>
        [DataMember]
        public int MinBuyQuantity { get; set; }
        /// <summary>
        /// 供应时间
        /// </summary>
        [DataMember]
        public DateTime? SupplierTime { get; set; }
        /// <summary>
        /// 操作人id
        /// </summary>
        [DataMember]
        public Guid CreatorUserId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 是否限制库存
        /// </summary>
        [DataMember]
        public bool IsInventory { get; set; }
        /// <summary>
        /// 销售开始时间
        /// </summary> 
        [DataMember]
        public DateTime? SaleStartTime { get; set; }
        /// <summary>
        /// 销售结束时间
        /// </summary> 
        [DataMember]
        public DateTime? SaleEndTime { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        [DataMember]
        public List<OptionParamForSpecialtyForPriceDto> OptionParamForSpecialtyForPriceDto { get; set; } = new List<OptionParamForSpecialtyForPriceDto>();
        /// <summary>
        /// 产品图片操作
        /// </summary>
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; } = new List<OptionParamForPictureDto>();

        /// <summary>
        /// 根据OptionParamForSpecialtyForParamDto这个解析规格为字符串【用于修改添加】
        /// </summary>
        [DataMember]
        public string SpecsStr
        {
            get
            {
                string result = string.Empty;
                if (OptionParamForSpecialtyForParamDto != null && OptionParamForSpecialtyForParamDto.Any())
                {
                    result = string.Join("|", OptionParamForSpecialtyForParamDto.Select(t => string.Format("{0},{1}", t.FormatName, t.Param)));
                }
                return result;
            }
        }
        /// <summary>
        /// 为SpecsStr提供可解析数据【用于修改添加】
        /// </summary>
        [DataMember]
        public List<OptionParamForSpecialtyForParamDto> OptionParamForSpecialtyForParamDto { get; set; }

        /// <summary>
        /// 为SpecsStr提供可解析数据【用于查询】
        /// </summary>
        [DataMember]
        public string SpecsStrForData { get; set; }

        /// <summary>
        /// 根据SpecsStr这个解析规格为字符串【用于查询】
        /// </summary>
        [DataMember]
        public List<OptionParamForSpecialtyForParamDto> OptionParamForSpecialtyForParamDtoByData
        {
            get
            {
                List<OptionParamForSpecialtyForParamDto> result = new List<Product.OptionParamForSpecialtyForParamDto>();
                if (!string.IsNullOrEmpty(SpecsStrForData))
                {
                    string[] specsStrForList = SpecsStrForData.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (specsStrForList != null && specsStrForList.Length > 0)
                    {
                        foreach (var item in specsStrForList)
                        {
                            result.Add(new OptionParamForSpecialtyForParamDto()
                            {
                                FormatName = item.Split(',')[0],
                                Param = item.Split(',')[1]
                            });
                        }
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 拒绝理由    
        /// </summary>
        [DataMember]
        public string Reason { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 分类名称【用于详情】
        /// </summary>
        [DataMember]
        public string ProductCategoriesName { get; set; }
        /// <summary>
        /// 品种名称【用于详情】
        /// </summary>
        [DataMember]
        public string VarietiesName { get; set; }

        #region 供应商信息
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }
        #endregion
    }

    /// <summary>
    /// 农产品价格添加参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForSpecialtyForPriceDto
    {
        /// <summary>
        /// 价格名称
        /// </summary>
        [DataMember]
        public string PriceName { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }
        /// <summary>
        /// 价格类型
        /// </summary>
        [DataMember]
        public int PriceClassify { get; set; }
    }

    /// <summary>
    /// 农产品价格添加参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForSpecialtyForParamDto
    {
        /// <summary>
        /// 规格名
        /// </summary>
        [DataMember]
        public string FormatName { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        [DataMember]
        public string Param { get; set; }
    }

    /// <summary>
    /// 管理后台农产品分类，名称，单位操作参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForSpecialtyForAdminDto : OptionParamForUserInfoForDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public int Classify { get; set; }
        /// <summary>
        /// 操作值
        /// </summary>
        [DataMember]
        public string Key { get; set; }
        /// <summary>
        /// 操作值Id
        /// </summary>
        [DataMember]
        public Guid KeyId { get; set; }
    }

    [Serializable]
    [DataContract]
    public class OptionParamForUserInfoForDto
    {
        /// <summary>
        /// 操作人id
        /// </summary>
        [DataMember]
        public Guid CreatorUserId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }
    }

    /// <summary>
    /// 管理后台农产品名称
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductForSpecialtyNameForAdminDto
    {
        /// <summary>
        /// Id
        /// </summary>
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
        public string CateName { get; set; }
        /// <summary>
        /// 农产品分类id
        /// </summary>
        [DataMember]
        public Guid CateId { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }
    }

}
