using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 订单流程类型
    /// </summary>
   public enum OrderProcessClassifyEnum
    {
        [Description("下单")]
        CreateOrder = 1,
        [Description("退款")]
        Refund = 2,
    }
}
