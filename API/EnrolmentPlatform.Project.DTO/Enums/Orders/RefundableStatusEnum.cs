using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Orders
{
    /// <summary>
    /// 退换货操作类型
    /// </summary>
    public enum RefundableOptionClassifyEnum
    {
        [Description("退货")]
        ReturnGoods = 1,
        [Description("换货")]
        ExchangeGoods = 2,
    }
    /// <summary>
    /// 退换货状态
    /// </summary>
    public enum RefundableStatusEnum
    {
        [Description("待处理")]
        StayHandle = 1,
        [Description("处理中")]
        Handling = 2,
        [Description("处理完成")]
        Handled = 3,
        [Description("已取消")]
        Cancel = 4,
    }
}
