using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Product;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 店铺DTO
    /// </summary>
    public class ShopDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 店面名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 完整地址名称
        /// </summary>
        public string FullAddressName { get; set; }

        /// <summary>
        /// 地址Id
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// 菜系Id
        /// </summary>
        public Guid CuisineId { get; set; }

        /// <summary>
        /// 菜系名称
        /// </summary>
        public string CuisineName { get; set; }

        /// <summary>
        /// Tel1
        /// </summary>
        public string Tel1 { get; set; }

        /// <summary>
        ///Tel2
        /// </summary>
        public string Tel2 { get; set; }

        /// <summary>
        /// 营业日：1,2,3,4,5,6,7 （以英文逗号分隔）枚举：WeekDayEnum
        /// </summary>
        public string OpenDay { get; set; }

        /// <summary>
        /// 营业开始时间格式：hh:mm (08:30)
        /// </summary>
        public string OpenTime { get; set; }

        /// <summary>
        /// 营业结束时间格式：hh:mm (08:30)
        /// </summary>
        public string CloseTime { get; set; }

        /// <summary>
        /// 服务设施Ids
        /// </summary>
        public string ServiceFacilitiesIds { get; set; }
        /// <summary>
        /// 类型【1：农产品】【2：票务产品】【3：餐饮】 枚举：ProductClassifyEnum
        /// </summary>
        public ProductClassifyEnum Classify { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态：【1：待审核】【2：已开业】【3：已停业】【4审核拒绝】
        /// </summary>
        public StatusForShopEnum Status { get; set; }

        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary> 
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreatorAccount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { set; get; }

        /// <summary>
        /// 审核说明
        /// </summary>
        public string AuditComment { set; get; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<ShopPicDto> FileList { set; get; }
    }

    /// <summary>
    /// 店铺图片DTO
    /// </summary>
    public class ShopPicDto
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// 是否是封面
        /// </summary>
        public bool IsCover { set; get; }

        /// <summary>
        /// 文件外键
        /// </summary>
        public Guid ForeignKeyId { set; get; }
    }

    /// <summary>
    /// 店铺查询DTO
    /// </summary>
    public class ShopSearchDto : GridDataRequest
    {
        /// <summary>
        /// 状态
        /// </summary>
        public StatusForShopEnum? Status { set; get; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { set; get; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { set; get; }
    }

    /// <summary>
    /// 审核DTO
    /// </summary>
    public class AuditDto
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
       public List<Guid> ShopIdList { set; get; }

        /// <summary>
        /// 是否通过
        /// </summary>
       public bool IsSucess { set; get; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Comment { set; get; }
    }
}
