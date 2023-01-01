using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 证件类型
    /// </summary>
    public enum ImageTypeEnum
    {
        /// <summary>
        /// 身份证正面
        /// </summary>
        [Description("身份证正面")]
        IDCard1 = 1,

        /// <summary>
        /// 身份证反面
        /// </summary>
        [Description("身份证反面")]
        IDCard2 = 2,

        /// <summary>
        /// 两寸蓝底
        /// </summary>
        [Description("两寸蓝底")]
        LiangCunLanDi = 3,

        /// <summary>
        /// 毕业证
        /// </summary>
        [Description("毕业证")]
        BiYeZheng = 4,

        /// <summary>
        /// 社保/居住证正
        /// </summary>
        [Description("社保/居住证正")]
        JuZhuZheng1 = 5,

        /// <summary>
        /// 社保/居住证反
        /// </summary>
        [Description("社保/居住证反")]
        JuZhuZheng2 = 6,

        /// <summary>
        /// 教育部学历证书电子备案表
        /// </summary>
        [Description("教育部学历证书电子备案表")]
        XueLiZhengShu = 7,

        /// <summary>
        /// 录取通知书
        /// </summary>
        [Description("录取通知书")]
        LuQuTongZhiShu = 8
    }
}
