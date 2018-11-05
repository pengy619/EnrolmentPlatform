using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Product
{
    /// <summary>
    /// 餐饮参数类型 枚举
    /// </summary>
    public enum CateringParamClassifyEnum
    {
        /// <summary>
        /// 菜系
        /// </summary>
        [Description("菜系")]
        FoodSeries = 1,
        /// <summary>
        /// 服务设施
        /// </summary>
        [Description("服务设施")]
        ServiceFacilities = 2
    }
}
