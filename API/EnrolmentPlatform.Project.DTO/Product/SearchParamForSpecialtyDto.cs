using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 农产品列表搜索条件参数
    /// </summary>
    public class SearchParamForSpecialtyDto : GridDataRequest
    {
        /// <summary>
        /// 供应商id
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public Guid? ProductCategoriesId { get; set; }
        /// <summary>
        /// 销售模式
        /// </summary>
        public int? SalesModel { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 品种名
        /// </summary>
        public string VarietiesName { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 不包含的状态
        /// </summary>
        public int StatusForNo { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
    }

    public class SearchParamForCategoriesDto : GridDataRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int Classify { get; set; }
    }

    /// <summary>
    /// 景点列表搜索条件参数
    /// </summary>
    public class SearchParamForTicketDto : GridDataRequest
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } 
        /// <summary>
        /// 景区类型
        /// </summary>
        public Guid ScenicClassifyId { get; set; }
        /// <summary>
        /// 景区级别
        /// </summary>
        public Guid ScenicLevelId { get; set; }
        /// <summary>
        /// 景点名称
        /// </summary>
        public string ScenicSportName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
    }

    /// <summary>
    /// 票务列表搜索条件参数
    /// </summary>
    public class SearchParamForTicketMngDto : GridDataRequest
    {
        /// <summary>
        /// 票型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public int SupplierType { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        public Guid ProductCateId { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public Guid ThemeId { get; set; }
        /// <summary>
        /// 门票名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 供应商名
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 不包含的状态
        /// </summary>
        public int StatusForNo { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
    }

    /// <summary>
    /// 票务销售 搜索条件
    /// </summary>
    public class SearchParamForTicketSalesDto : GridDataRequest
    {
        /// <summary>
        ///  票务类别  1：单票，2：套票
        /// </summary>
        public TypeForTicketEnum? TicketCategories { get; set; }

        /// <summary>
        /// 票种
        /// </summary>
        public Guid? TicketTypeId { get; set; }

        /// <summary>
        /// 主题 
        /// </summary>
        public Guid? ThemeId { get; set; }

        /// <summary>
        /// 景点
        /// </summary>
        public List<string> AttractionsIds { get; set; }

        /// <summary>
        ///  游乐项目  
        /// </summary>
        public List<string> AmusementItems { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///  供应商类型  
        /// </summary>
        public SupplierTypeEnum? SupplierType { get; set; }

    }


    /// <summary>
    ///  票务销售列表DTO
    /// </summary>
    public class ProductForTicketSalesDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// 编号
        /// </summary> 
        public string ProductNumber { get; set; }

        /// <summary>
        /// 门票 类别
        /// </summary>
        public TypeForTicketEnum TicketClassify { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 主题ID
        /// </summary>
        public Guid ThemeId { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// 票种ID
        /// </summary>
        public Guid TicketTypeId { get; set; }

        /// <summary>
        /// 票种
        /// </summary>
        public string CateName { get; set; }


        /// <summary>
        /// 景点Id 以逗号分隔
        /// </summary>
        public string ScenicSpotIdStr { get; set; }
        /// <summary>
        /// 游乐项目Id 以逗号分隔
        /// </summary>
        public string AmusementProjectIdStr { get; set; }

        /// <summary>
        /// 供应商类型
        /// </summary>
        public SupplierTypeEnum SupplierType { get; set; }


        public string SupplierTypeName
        {
            get
            { return EnumDescriptionHelper.GetDescription(SupplierType); }
        }

        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public ProductStatusEnum Status { get; set; }


        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; } = 0;

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// 游玩日期
        /// </summary>
        public string PlayDate { get; set; } = DateTime.Now.ToString("yyyy-M-dd");

        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(Status);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TicketClassifyStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(TicketClassify);
            }
        }

        public TicketDetailDto TicketDetail { get; set; }

        /// <summary>
        /// 购买数量 默认为1
        /// </summary>
        public int Quantity { get; set; } = 1;

    }


    public class TicketDetailDto
    {
        /// <summary>
        /// 预订说明
        /// </summary>
        public string BookingInstructions { get; set; }
        /// <summary>
        /// 使用说明
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// 游乐项目
        /// </summary>
        public string AmusementItem { get; set; }

        /// <summary>
        /// 景点
        /// </summary>
        public string Attractions { get; set; }

        /// <summary>
        ///  供应商信息，格式：此产品由XXX供应商提供，联系电话137123458920
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 供应商联系电话
        /// </summary>
        public string Phone { get; set; }
    }

    /// <summary>
    ///  票务价格 Dto
    /// </summary>
    public  class TicketPriceDto
    {
        /// <summary>
        /// 价格
        /// </summary>
        public  decimal Price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PlayDate
        {
            get { return PlayDay.ToString("yyyy-M-dd"); }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime PlayDay { get; set; }

    }

    /// <summary>
    /// 游乐项目列表搜索条件参数
    /// </summary>
    public class SearchParamForPlayMngDto : GridDataRequest
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public int SupplierType { get; set; }
        /// <summary>
        /// 景点范围
        /// </summary>
        public int ScenicRange { get; set; }
        /// <summary>
        /// 游乐项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 景点项目名称
        /// </summary>
        public string ScenicSpotName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateName { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }
    }
}
