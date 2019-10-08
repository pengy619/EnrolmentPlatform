using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 订单图片状态
    /// </summary>
    public enum OrderImageStatusEnum
    {
        /// <summary>
        /// 缺毕业证
        /// </summary>
        [Description("缺毕业证")]
        DefBiYeZheng = 1,

        /// <summary>
        /// 缺蓝底
        /// </summary>
        [Description("缺蓝底")]
        DefLanDi = 2,

        /// <summary>
        /// 缺电子备案
        /// </summary>
        [Description("缺电子备案")]
        DefDianZiBeiAn = 3,

        /// <summary>
        /// 缺异地证明
        /// </summary>
        [Description("缺异地证明")]
        DefYiDiZhengMing = 4,
    }
}
