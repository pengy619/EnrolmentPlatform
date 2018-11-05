using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 景点列表信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ListForTicketForScenicSportDataDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public int ScenicSportNumber { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string ScenicSportName { get; set; }
        /// <summary>
        /// 景点类型
        /// </summary>
        [DataMember]
        public string ScenicClassifyStr { get; set; }
        /// <summary>
        /// 景点等级
        /// </summary>
        [DataMember]
        public string ScenicLevelStr { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [DataMember]
        public string Contact { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 景点电话
        /// </summary>
        [DataMember]
        public string ScenicTel { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Status 【1：禁用】【2：启用】【3：草稿】
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusForTikcetEnum)Status);
            }
        }
    }

    /// <summary>
    /// 景点新增修改查询
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionForTicketForScenicSportDataDto : OptionParamForUserInfoForDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public int ScenicSportNumber { get; set; }
        /// 名称
        /// </summary>
        [DataMember]
        public string ScenicSportName { get; set; }
        /// <summary>
        /// 景点等级
        /// </summary>
        [DataMember]
        public Guid ScenicLevelId { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        [DataMember]
        public string Coordinate { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [DataMember]
        public string Contact { get; set; }
        /// <summary>
        /// 景点电话
        /// </summary>
        [DataMember]
        public string ScenicTel { get; set; }
        /// <summary>
        /// 景点类型
        /// </summary>
        [DataMember]
        public Guid ScenicClassifyId { get; set; }
        /// <summary>
        /// 预订须知
        /// </summary>
        [DataMember]
        public string OrderNotes { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// 交通指南
        /// </summary>
        [DataMember]
        public string TrafficGuide { get; set; }
        /// <summary>
        /// Status 【1：禁用】【2：启用】【3：草稿】
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 审核结果
        /// </summary>
        [DataMember]
        public string AuditResult { get; set; }

        /// <summary>
        /// 开放时间
        /// </summary>
        [DataMember]
        public string OpenTime { get; set; }
        /// <summary>
        /// 关闭时间
        /// </summary>
        [DataMember]
        public string CloseTime { get; set; }

        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusForTikcetEnum)Status);
            }
        }
        /// <summary>
        /// 图片
        /// </summary> 
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; }
    }


    /// <summary>
    /// 票务列表信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ListForProductForTicketDataDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public string ProductNumber { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [DataMember]
        public int TicketClassify { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        [DataMember]
        public string ThemeName { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        [DataMember]
        public string CateName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        [DataMember]
        public int SupplierType { get; set; }
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
        [DataMember]
        public string TicketClassifyStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((TypeForTicketEnum)TicketClassify);
            }
        }
        [DataMember]
        public string SupplierTypeStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((SupplierTypeEnum)SupplierType);
            }
        }
    }

    /// <summary>
    /// 票务操作参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForTicketForDataDto
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        [DataMember]
        public OptionParamForTicketForDataForBasicDto OptionParamForTicketForDataForBasicDto { get; set; }
        /// <summary>
        /// 详细信息
        /// </summary>
        [DataMember]
        public OptionParamForTicketForDataForXiangDto OptionParamForTicketForDataForXiangDto { get; set; }
    }
    /// <summary>
    /// 票务操作参数-基础信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForTicketForDataForBasicDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
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
        /// 产品分类Id
        /// </summary>
        [DataMember]
        public Guid ProductCategoriesId { get; set; }
        /// <summary>
        ///产品分类名称
        /// </summary>
        [DataMember]
        public string ProductCateName { get; set; }
        /// <summary>
        /// 库存类型
        /// </summary>
        [DataMember]
        public int InventoryClassify { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DataMember]
        public Guid CreatorUserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }
        /// <summary>
        ///创建人时间
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 理由
        /// </summary>
        [DataMember]
        public string Reason { get; set; }
        /// <summary>
        /// 下架时间
        /// </summary>
        [DataMember]
        public DateTime OffSaleTime { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        [DataMember]
        public DateTime OnSaleTime { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DataMember]
        public DateTime ApplyOnSaleTime { get; set; }

        /*门票表数据*/
        /// <summary>
        /// 票务类型：【1：单票】【2：套票】
        /// </summary>
        [DataMember]
        public int TicketClassify { get; set; }
        /// <summary>
        /// 主题Id
        /// </summary>
        [DataMember]
        public Guid ThemeId { get; set; }
        /// <summary>
        /// 主题名称
        /// </summary>
        [DataMember]
        public string ThemeName { get; set; }
        /// <summary>
        /// 景点Id 以逗号分隔
        /// </summary>
        [DataMember]
        public string ScenicSpotIdStr { get; set; }
        /// <summary>
        /// 景点名称 以逗号分隔
        /// </summary>
        [DataMember]
        public string ScenicSpotNameStr { get; set; }
        [DataMember]
        public List<OptionParamForKeyValueDto> ScenicSpotForKeyValueDto { get; set; }
        /// <summary>
        /// 游乐项目Id 以逗号分隔
        /// </summary>
        [DataMember]
        public string AmusementProjectIdStr { get; set; }
        [DataMember]
        public string AmusementProjectNameStr { get; set; }
        [DataMember]
        public List<OptionParamForKeyValueDto> AmusementProjectForKeyValueDto { get; set; }
        /// <summary>
        /// 包含成人数
        /// </summary>
        [DataMember]
        public int AdultNumber { get; set; }
        /// <summary>
        /// 包含儿童数
        /// </summary>
        [DataMember]
        public int ChildNumber { get; set; }
        /// <summary>
        /// 购买最大年龄
        /// </summary>
        [DataMember]
        public int BuyMaxAge { get; set; }
        /// <summary>
        /// 购买最小年龄
        /// </summary>
        [DataMember]
        public int BuyMinAge { get; set; }
        /// <summary>
        /// 提前预订时间 天
        /// </summary>
        [DataMember]
        public int BookTimeLimitDay { get; set; }
        /// <summary>
        /// 提前预订时间 小时
        /// </summary>
        [DataMember]
        public int BookTimeLimitHour { get; set; }
        /// <summary>
        /// 提前预订时间 分钟
        /// </summary>
        [DataMember]
        public int BookTimeLimitMinute { get; set; }
        /// <summary>
        /// 退票规则【1：不可退】【2：条件退】
        /// </summary>
        [DataMember]
        public int RefundRule { get; set; }
        /// <summary>
        /// 游玩日期前 是否
        /// </summary>
        [DataMember]
        public bool IsBefore { get; set; }
        /// <summary>
        /// 可退时间（天）
        /// </summary>
        [DataMember]
        public int RefundDay { get; set; }
        [DataMember]
        public int SupplierType { get; set; }

        /// <summary>
        /// 提前预订时间 小时
        /// </summary>
        [DataMember]
        public string BookTimeLimitHourStr
        {
            get
            {
                return BookTimeLimitHour < 10 ? ("0" + BookTimeLimitHour.ToString()) : BookTimeLimitHour.ToString();
            }
        }
        /// <summary>
        /// 提前预订时间 分钟
        /// </summary>
        [DataMember]
        public string BookTimeLimitMinuteStr
        {
            get
            {
                return BookTimeLimitMinute < 10 ? ("0" + BookTimeLimitMinute.ToString()) : BookTimeLimitMinute.ToString();
            }
        }
        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ProductStatusEnum)Status);
            }
        }
        [DataMember]
        public string TicketClassifyStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((TypeForTicketEnum)TicketClassify);
            }
        }
        [DataMember]
        public string RefundRuleStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((RefundPriceEnum)RefundRule);
            }
        }
    }
    /// <summary>
    /// 票务操作参数-详细信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForTicketForDataForXiangDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }
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
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; } = new List<OptionParamForPictureDto>();
        /// <summary>
        /// 预订说明
        /// </summary>
        [DataMember]
        public string BookDescription { get; set; }
        /// <summary>
        /// 使用说明
        /// </summary>
        [DataMember]
        public string UseDescription { get; set; }
    }

    /// <summary>
    /// 票务操作参数-班期
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForTicketForDataForDateDto
    {
        /// <summary>
        /// 产品id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }
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
        /// 开始日期
        /// </summary>
        [DataMember]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 星期几
        /// </summary>
        [DataMember]
        public List<int> Weeks { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }

        /// <summary>
        /// 可以操作的时间，
        /// </summary>
        public List<DateTime> DateTimes
        {
            get
            {
                List<DateTime> dateTimes = new List<DateTime>();
                if (!StartTime.Equals(DateTime.MinValue) && !EndTime.Equals(DateTime.MinValue) && Weeks != null && Weeks.Any())
                {
                    for (DateTime i = StartTime; i <= EndTime; i = i.AddDays(1))
                    {
                        if (Weeks.Contains((int)i.DayOfWeek))
                        {
                            dateTimes.Add(i);
                        }
                    }
                }
                return dateTimes;
            }
        }
    }

    /// <summary>
    /// 票务操作参数-班期查询
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchParamForTicketForScheduleDto
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
        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// 票务操作参数-班期查询结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchParamForTicketForScheduleForDataDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public string PlayDay { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        [DataMember]
        public int InventCount { get; set; }
        /// <summary>
        /// 已售库存
        /// </summary>
        [DataMember]
        public int SoldInventory { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        [DataMember]
        public decimal Price { get; set; }
        /// <summary>
        /// 可用库存
        /// </summary>
        [DataMember]
        public int Stock
        {
            get
            {
                return InventCount - SoldInventory;
            }
        }
    }

    /// <summary>
    /// 用于对应包含景点，包含游玩项目
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForKeyValueDto
    {
        /// <summary>
        /// Key
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        [DataMember]
        public string Value { get; set; }

    }

    /// <summary>
    /// 票务列表信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ListForProductForTicketForClientDataDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public string ProductNumber { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [DataMember]
        public int TicketClassify { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        [DataMember]
        public string ThemeName { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        [DataMember]
        public string CateName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 主图
        /// </summary>
        [DataMember]
        public string PictureUrl { get; set; }
        /// <summary>
        /// 最近信息
        /// </summary>
        [DataMember]
        public SearchParamForTicketForScheduleForDataDto Date { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        [DataMember]
        public int Stock
        {
            get
            {
                return Date != null ? Date.Stock : 0;

            }
        }
        [DataMember]
        public string TicketClassifyStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((TypeForTicketEnum)TicketClassify);
            }
        }
    }
}
