using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.File
{

    public enum FileClassifyEnum
    {

        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Picture = 1,

        /// <summary>
        /// 文件
        /// </summary>
        [Description("文件")]
        File = 2
    }

    public enum ForeignKeyClassifyEnum
    {

        /// <summary>
        /// 产品
        /// </summary>
        [Description("产品")]
        Product = 1,

        /// <summary>
        /// 品种
        /// </summary>
        [Description("品种")]
        ProductCategory = 2,

        /// <summary>
        /// 企业资质
        /// </summary>
        [Description("企业资质")]
        Enterprise = 3,

        /// <summary>
        /// Banner轮播
        /// </summary>
        [Description("Banner轮播")]
        Banner = 4,
        /// <summary>
        /// 游乐项目
        /// </summary>
        [Description("游乐项目")]
        AmusementProject = 5,

        /// <summary>
        /// 景点图片
        /// </summary>
        [Description("景点图片")]
        ScenicSport = 6,
        /// <summary>
        /// 退换货记录
        /// </summary>
        [Description("退换货记录")]
        RefundableRecord = 7,
        /// <summary>
        /// 店铺图片
        /// </summary>
        [Description("店铺图片")]
        Shop = 8,
        /// <summary>
        /// 菜（食物）图片
        /// </summary>
        [Description("菜（食物）图片")]
        Food = 9,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileBusinessTypeEnum
    {
        /// <summary>
        /// 营业执照
        /// </summary>
        [Description("营业执照")]
        License = 1,

        /// <summary>
        /// 身份证正面
        /// </summary>
        [Description("身份证正面")]
        IDCardPositive = 2,

        /// <summary>
        /// 身份证反面
        /// </summary>
        [Description("身份证反面")]
        IDCardNegative = 3,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("身份证反面")]
        Other = 4
    }
}
