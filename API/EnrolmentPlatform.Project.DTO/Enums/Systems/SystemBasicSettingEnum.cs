using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    public enum SystemBasicSettingEnum
    {
        /// <summary>
        /// 网站LOGO
        /// </summary>
        [Description("网站LOGO")]       
        WebsiteLogo = 1,

        /// <summary>
        /// 网站标题
        /// </summary>
        [Description("网站标题")]
        WebsiteTitle = 2,

        /// <summary>
        /// 网站关键字
        /// </summary>
        [Description("网站关键字")]
        WebsiteKW = 3,

        /// <summary>
        /// 网站描述
        /// </summary>
        [Description("网站描述")]
        WebsiteDescription = 4,

        /// <summary>
        /// b2c农产品取消时间（小时）
        /// </summary>
        [Description("b2c农产品取消时间（小时）")]
        B2CSpecialtyCancelTime = 5,

        /// <summary>
        /// b2c票务取消时间（小时）
        /// </summary>
        [Description("b2c票务取消时间（小时）")]
        B2CTicketCancelTime = 6,


        /// <summary>
        /// 游客中心票务取消时间（小时）
        /// </summary>
        [Description("游客中心票务取消时间（小时）")]
        TouristsCenterTicketCancelTime = 7,

        /// <summary>
        /// 农产品自动收货时间（天）
        /// </summary>
        [Description("农产品自动收货时间（天）")]
        SpecialtyAutoReceiptTime = 8,

        /// <summary>
        /// 提现范围（0-5000格式分隔）
        /// </summary>
        [Description("提现范围（0-5000格式分隔）")]
        WithdrawRange = 9,

        /// <summary>
        /// 自营供应商发布产品是否需要审核
        /// </summary>
        [Description("自营供应商发布产品是否需要审核")]
        IsNeedAdultBySelfSupplier = 10,

        /// <summary>
        /// 门票在游玩日期后多少天完成
        /// </summary>
        [Description("门票在游玩日期后多少天完成")]
        TikcetAutoComplete = 11,

        /// <summary>
        /// 预售订单尾款支付（小时）
        /// </summary>
        [Description("预售订单尾款支付（小时）")]
        PayRetainageAutoCancel = 12,

        /// <summary>
        /// H5网站标题
        /// </summary>
        [Description("H5网站标题")]
        WebSiteTitleForH5 = 13,
        /// <summary>
        /// 用户注册协议
        /// </summary>
        [Description("用户注册协议")]
        UserProtocol = 14,

        /// <summary>
        /// 餐饮订单自动取消时间（小时）
        /// </summary>
        [Description("餐饮订单自动取消时间（小时）")]
        CateringOrderAutoCancel = 15,
    }
}
