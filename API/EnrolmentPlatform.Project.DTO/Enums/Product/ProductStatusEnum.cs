using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Product
{
    /// <summary>
    /// 产品状态 枚举
    /// </summary>
    public enum ProductStatusEnum
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Draft = 1,
        /// <summary>
        /// 上架审核
        /// </summary>
        [Description("待审核")]
        Checking = 2,
        /// <summary>
        /// 已上架
        /// </summary>
        [Description("已上架")]
        OnSale = 3,
        /// <summary>
        /// 审核拒绝
        /// </summary>
        [Description("审核拒绝")]
        CheckNo = 4,
        /// <summary>
        /// 已下架
        /// </summary>
        [Description("已下架")]
        OffSale = 5
    }

    public enum ProductClassifyEnum
    {
        /// <summary>
        /// 农产品
        /// </summary>
        [Description("农产品")]
        Specialty = 1,
        /// <summary>
        /// 门票
        /// </summary>
        [Description("门票")]
        Ticket = 2,
        /// <summary>
        /// 餐饮
        /// </summary>
        [Description("餐饮")]
        Catering = 3
    }

    public enum ProductInventoryClassifyEnum
    {
        [Description("班期日历库存")]
        DateInventory = 1,
        [Description("总库存")]
        AllInventory = 2
    }

    public enum ProductPriceClassifyEnum
    {
        [Description("结算价")]
        SettlementPrice = 1,
        [Description("销售价")]
        DistributionPrice = 2,
        [Description("市场价")]
        MarkPrice = 3
    }

    public enum ProductSaleModelEnum
    {

        /// <summary>
        /// 现货
        /// </summary>
        [Description("现货")]
        SoldNow = 1,

        /// <summary>
        /// 预售
        /// </summary>
        [Description("预售")]
        Presale = 2
    }

    public enum PriceEnum
    {
        [Description("产品")]
        Product = 1,
        [Description("班期")]
        Schedule = 2
    }

    public enum InventoryEnum
    {
        [Description("产品")]
        Product = 1,
        [Description("班期")]
        Schedule = 2
    }


    public enum TikcetParamClassifyEnum
    {
        [Description("票主题")]
        Theme = 1,
        [Description("景点等级")]
        Level = 2,
        [Description("景点类型")]
        Cate = 3
    }

    /// <summary>
    /// 门票景点状态
    /// </summary>
    public enum StatusForTikcetEnum
    {
        [Description("已禁用")]
        Disabled = 1,
        [Description("已启用")]
        Enabled = 2,
        [Description("草稿")]
        Draft = 3
    }

    /// <summary>
    /// 门票类别
    /// </summary>
    public enum TypeForTicketEnum
    {
        /// <summary>
        /// 单票
        /// </summary>
        [Description("单票")]
        Single = 1,
        /// <summary>
        /// 套票
        /// </summary>
        [Description("套票")]
        Package = 2
    }

    /// <summary>
    /// 游乐项目状态 枚举
    /// </summary>
    public enum StatusForPlayProjectEnum
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Checking = 1,
        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("已启用")]
        Enable = 2,
        /// <summary>
        /// 审核拒绝
        /// </summary>
        [Description("审核拒绝")]
        CheckNo = 3,
        /// <summary>
        /// 已禁用
        /// </summary>
        [Description("已禁用")]
        DisEnable = 4
    }

    /// <summary>
    /// 景点范围 枚举
    /// </summary>
    public enum StatusForScenicRangeEnum
    {
        /// <summary>
        /// 景点内
        /// </summary>
        [Description("景点内")]
        In = 1,
        /// <summary>
        /// 景点外
        /// </summary>
        [Description("景点外")]
        Out = 2
    }

    /// <summary>
    /// 退款规则 枚举
    /// </summary>
    public enum RefundPriceEnum
    {
        /// <summary>
        /// 不可退
        /// </summary>
        [Description("不可退")]
        No = 1,
        /// <summary>
        /// 游玩日期前可退
        /// </summary>
        [Description("在游玩日期前")]
        Before = 2,
        /// <summary>
        /// 游玩日期后可退
        /// </summary>
        [Description("在游玩日期后")]
        After = 3
    }
    /// <summary>
    /// 班期状态 枚举
    /// </summary>
    public enum StatusForScheduleEnum
    {
        /// <summary>
        /// 开班
        /// </summary>
        [Description("开班")]
        Open = 1,
        /// <summary>
        /// 关班
        /// </summary>
        [Description("关班")]
        Close = 2
    }
    /// <summary>
    /// 推荐位置枚举
    /// </summary>
    public enum RecommendPositionEnum
    {
        /// <summary>
        /// B2C首页
        /// </summary>
        [Description("B2C首页")]
        HomeForB2C = 1,
        /// <summary>
        /// 农产品商城H5
        /// </summary>
        [Description("农产品商城H5")]
        SpecialtyShopMallForH5 = 2
    }
    /// <summary>
    /// 用餐人数
    /// </summary>
    public enum UsePeopleEnum
    {
        [Description("单人餐")]
        One = 1,
        [Description("双人餐")]
        Two = 2,
        [Description("3-4人")]
        Three = 3,
        [Description("5-6人")]
        Four = 4,
        [Description("7-8人")]
        Five = 5,
        [Description("9-10人")]
        Six = 6,
        [Description("10人以上")]
        Seven = 7
    }
    /// <summary>
    /// 退款规则
    /// </summary>
    public enum RefundRuleEnum
    {
        [Description("随时退")]
        AllTime = 0,
        [Description("过期不可退")]
        OutTimeNo = 1,
        [Description("不可退")]
        No = 2
    }
}
