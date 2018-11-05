using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public enum SystemMessageEnum
    {
        /// <summary>
        /// 订单改价 通知会员/供应商
        /// </summary>
        [Description("订单号：{0}价格己发生修改为{1}，原价为{2}，时间为{3}")]
        ChangePrice=1,
        /// <summary>
        /// 发货通知会员
        /// </summary>
        [Description("订单号：{0}己完成发货，物流信息为{1}{2}，时间为{3}")]
        DeliverGoods=2,
        /// <summary>
        /// 退换货处理 通知会员
        /// </summary>
        [Description("退货单号：{0}卖家己开始确认处理！时间为{1}")]
        RefundRecordHandle=3,
        /// <summary>
        /// 登录密码修改 通知会员
        /// </summary>
        [Description("会员密码修改成功，修改时间为{1}")]
        ChangeLoginPassWord=4,
        /// <summary>
        /// 登录密码修改 通知会员
        /// </summary>
        [Description("结算单己生成，结算金额为{0}，结算时间为{1}")]
        Settlement = 5
    }
}
