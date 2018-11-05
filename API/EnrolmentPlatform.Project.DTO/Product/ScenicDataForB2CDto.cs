using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 游玩项目，景点列表数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class ListForScenicDataForB2CDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 标题（景点名称/游玩项目名称）
        /// </summary>
        [DataMember]
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Descript { get; set; }
        /// <summary>
        /// 主图
        /// </summary>
        [DataMember]
        public string PictureUrl { get; set; }
    }

    /// <summary>
    /// 游玩项目，景点列表详情
    /// </summary>
    [Serializable]
    [DataContract]
    public class DetailForScenicSpotForB2CDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 标题（景点名称/游玩项目名称）
        /// </summary>
        [DataMember]
        public string Title { get; set; }
        /// <summary>
        /// 编号（景点名称/游玩项目编号）
        /// </summary>
        [DataMember]
        public int Number { get; set; }
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
        /// 景点id
        /// </summary>
        [DataMember]
        public string ScenicSpotIdStr { get; set; }

        /// <summary>
        /// 景点类型
        /// </summary>
        [DataMember]
        public string ScenicClassify { get; set; }
        /// <summary>
        /// 景点联系人
        /// </summary>
        [DataMember]
        public string Contact { get; set; }
        /// <summary>
        /// 景点电话
        /// </summary>
        [DataMember]
        public string ScenicTel { get; set; }
        /// <summary>
        /// 景点等级
        /// </summary>
        [DataMember]
        public string ScenicLevel { get; set; }
        /// <summary>
        /// 景点范围
        /// </summary>
        [DataMember]
        public string ScenicRange { get; set; }

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
        /// <summary>
        /// 图片
        /// </summary> 
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; }
    }


    /// <summary>
    /// 游玩项目，景点详情门票数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class DetailForTicketDataByScenicOrPlayForB2CDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 门票名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        [DataMember]
        public decimal MinMarkPrice { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        [DataMember]
        public string CateName { get; set; }
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
        /// 班期数量，大于0表示有班期
        /// </summary>
        [DataMember]
        public int ScheduleCount { get; set; }
        /// <summary>
        ///  
        /// </summary>
        [DataMember]
        public string BookStr
        {
            get
            {
                string result = string.Empty;
                if (BookTimeLimitDay > 0)
                {
                    result += string.Format("游玩前{0}天", BookTimeLimitDay.ToString());
                }
                else
                {
                    result += "游玩当天";
                }
                if (BookTimeLimitHour > 0)
                {
                    result += string.Format("{0}:{1}前", BookTimeLimitHour.ToString(), BookTimeLimitMinute.ToString());
                }
                else
                {
                    result += "23:59前";
                }
                return result;
            }
        }
        [DataMember]
        public string MinMarkPriceStr
        {
            get
            {
                return string.Format("￥{0}", MinMarkPrice.ToString("f2"));
            }
        }
    }

    /// <summary>
    /// 门票汇总-页面加载数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchTicketForQueryForB2CDto
    {
        /// <summary>
        /// 门票类型
        /// </summary>
        [DataMember]
        public List<KeyValuePair<int, string>> TypeList { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        [DataMember]
        public List<OptionParamForKeyValueDto> CateList { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        [DataMember]
        public List<OptionParamForKeyValueDto> ThemeList { get; set; }
        /// <summary>
        /// 关键词（游乐项目）
        /// </summary>
        [DataMember]
        public List<OptionParamForKeyValueDto> KeyForPlayList { get; set; }
        /// <summary>
        /// 关键词（景点）
        /// </summary>
        [DataMember]
        public List<OptionParamForKeyValueDto> KeyForSpotList { get; set; }
    }

    /// <summary>
    /// 门票汇总-用于搜索参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchTicketForParamForB2CDto : GridDataRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public List<int> TypeList { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        [DataMember]
        public List<Guid> CateList { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        [DataMember]
        public List<Guid> ThemeList { get; set; }
        /// <summary>
        /// 关键词（游乐项目）
        /// </summary>
        [DataMember]
        public string KeyForPlay { get; set; }
        /// <summary>
        /// 关键词（景点）
        /// </summary>
        [DataMember]
        public string KeyForSpot { get; set; }
    }

    /// <summary>
    /// 门票汇总-用于搜索结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class SearchTicketForDataResultForB2CDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
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
        /// 票种
        /// </summary>
        [DataMember]
        public string CategoryName { get; set; }
        /// <summary>
        /// 主图
        /// </summary>
        [DataMember]
        public string PictureUrl { get; set; }
        /// <summary>
        /// 销售量
        /// </summary>
        [DataMember]
        public int OrderCount { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        [DataMember]
        public decimal MinPrice { get; set; }
    }

    /// <summary>
    /// 门票汇总详情
    /// </summary>
    [Serializable]
    [DataContract]
    public class DetailTicketForDataResultForB2CDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
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
        /// 票种
        /// </summary>
        [DataMember]
        public string CategoryName { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        [DataMember]
        public decimal MinPrice { get; set; }
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
        /// <summary>
        /// 库存数量
        /// </summary>
        [DataMember]
        public int ScheduleCount { get; set; }
        /// <summary>
        /// 产品状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string BookStr
        {
            get
            {
                string result = string.Empty;
                if (BookTimeLimitDay > 0)
                {
                    result += string.Format("游玩前{0}天", BookTimeLimitDay.ToString());
                }
                else
                {
                    result += "游玩当天";
                }
                if (BookTimeLimitHour > 0)
                {
                    result += string.Format("{0}:{1}前", BookTimeLimitHour.ToString(), BookTimeLimitMinute.ToString());
                }
                else
                {
                    result += "23:59前";
                }
                return result;
            }

        }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; }
        /// <summary>
        /// 包含景点
        /// </summary>
        [DataMember]
        public List<OptionParamForKeyValueDto> SpotList { get; set; }
        /// <summary>
        /// 包含游玩项目
        /// </summary>
        [DataMember]
        public List<OptionParamForKeyValueDto> PlayList { get; set; }

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
    /// 填写订单-班期数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataForDateForTicketForFillForB2CDto
    {
        [DataMember]
        public List<SearchParamForTicketForScheduleForDataDto> Data { get; set; }
    }


    /// <summary>
    /// 填写订单-加载数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataForTicketForFillForB2CDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        [DataMember]
        public int ScheduleCount { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        [DataMember]
        public List<KeyValuePair<int, string>> CredentialsTypeEnum { get; set; }
        /// <summary>
        /// 常用出行人
        /// </summary>
        [DataMember]
        public List<ContactsDto> ContactsDto { get; set; }
        /// <summary>
        /// 最近班期
        /// </summary>
        [DataMember]
        public SearchParamForTicketForScheduleForDataDto SearchParamForTicketForScheduleForDataDto { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        [DataMember]
        public decimal MinDistributionPrice { get; set; }
    }

    /// <summary>
    /// 游玩项目，景点详情门票数据H5
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataForTicketByScenicOrPlayForH5
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 门票名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        [DataMember]
        public decimal MinMarkPrice { get; set; }
        /// <summary>
        /// 票种
        /// </summary>
        [DataMember]
        public string CateName { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public string MinMarkPriceStr
        {
            get
            { 
                return string.Format("￥{0}", MinMarkPrice.ToString("f2"));
            } 
        }
    }
}
