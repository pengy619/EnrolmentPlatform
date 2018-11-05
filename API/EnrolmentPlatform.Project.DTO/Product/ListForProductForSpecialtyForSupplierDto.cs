using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Product
{
    [Serializable]
    [DataContract]
    public class ListForProductForSpecialtyForSupplierDto
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary> 
        [DataMember]
        public string ProductNumber { get; set; }
        /// <summary>
        /// 产品分类ID
        /// </summary> 
        [DataMember]
        public Guid ProductCategoriesId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary> 
        [DataMember]
        public string CateName { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary> 
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 品种ID
        /// </summary> 
        [DataMember]
        public Guid VarietiesId { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary> 
        [DataMember]
        public string VarietiesName { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary> 
        [DataMember]
        public string SpecsStr { get; set; }
        /// <summary>
        /// 销售单位
        /// </summary> 
        [DataMember]
        public string SaleslUnit { get; set; }
        /// <summary>
        /// 最低市场价
        /// </summary> 
        [DataMember]
        public decimal MinMarkPrice { get; set; }
        /// <summary>
        /// 销售模式【1：现售】【2：预售】
        /// </summary> 
        [DataMember]
        public int SalesModel { get; set; }
        /// <summary>
        /// 状态【1：未上架】【2：上架审核】【3：已上架】【4：拒绝上架】
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary> 
        [DataMember]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 供应商Name
        /// </summary> 
        [DataMember]
        public string EnterpriseName { get; set; }
        /// <summary>
        /// CreatorTime
        /// </summary> 
        [DataMember]
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// IsMarket上市
        /// </summary> 
        [DataMember]
        public bool IsMarket { get; set; }
        /// <summary>
        /// 库存数
        /// </summary> 
        [DataMember]
        public string Inventory { get; set; }
        /// <summary>
        /// 状态中文
        /// </summary>
        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ProductStatusEnum)Status);
            }
        }
        /// <summary>
        /// 销售模式中文
        /// </summary>
        [DataMember]
        public string SalesModelStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ProductSaleModelEnum)SalesModel);
            }
        }
        /// <summary>
        /// 销售模式中文
        /// </summary>
        [DataMember]
        public string MinMarkPriceStr
        {
            get
            {
                return MinMarkPrice == 0 ? "" : "￥" + MinMarkPrice.ToString();
            }
        }
        /// <summary>
        /// 已售
        /// </summary> 
        [DataMember]
        public string SoldQuantity { get; set; }
        /// <summary>
        /// 可售
        /// </summary> 
        [DataMember]
        public string SaleQuantity { get; set; }
    }
}
