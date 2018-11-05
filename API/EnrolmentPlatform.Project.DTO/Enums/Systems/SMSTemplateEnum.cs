using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{
    /// <summary>
    /// 短信内容
    /// </summary>
    public enum SMSTemplateEnum
    {
        /// <summary>
        /// 农产品审核通过通知供应商 签名【智联智行】
        /// </summary>
        [Description("{0}您{1}提交的农产品{2}己审核通过，己上架销售，详情请登录系统查看！")]
        AduitSuccessForSpecialtyToSupplier = 1,
        /// <summary>
        /// 农产品审核失败通知供应商
        /// </summary>
        [Description("{0}您{1}提交的农产品{2}己被拒绝，详情请登录系统查看！")]
        AduitFailForSpecialtyToSupplier = 2,
        /// <summary>
        /// 农产品被总部下架通知供应商
        /// </summary>
        [Description("{0}您的农产品{1}己被景区运营方下架，将禁止销售，详情请咨询景区运营方！")]
        OffSaleForSpecialtyByPlatformToSupplier = 3,
        /// <summary>
        /// 农产品现货支付完成通知供应商
        /// </summary>
        [Description("{0}{1}己完成支付，请及时发货，详情请登录系统查看!")]
        PaySuccessOrderForSpecialtyToSupplier = 4,
        /// <summary>
        ///农产品发起退款申请通知供应商
        /// </summary>
        [Description("{0}{1}己发起退款，详情请登录系统查看!")]
        ApplyRefundOrderForSpecialtyToSupplier = 5,
        /// <summary>
        ///农产品支付定金通知供应商
        /// </summary>
        [Description("{0}{1}己支付订金，详情请登录系统查看!")]
        PayDepositOrderForSpecialtyToSupplier = 6,
        /// <summary>
        ///农产品支付尾款通知供应商
        /// </summary>
        [Description("{0}{1}己支付尾款，请及时发货，详情请登录系统查看!")]
        PayRetainageOrderForSpecialtyToSupplier = 7,
        /// <summary>
        ///农产品发起申请退换货记录通知供应商
        /// </summary>
        [Description("{0}{1}己提交退换货申请，请及时处理，详情请登录系统查看!")]
        ApplyRefundRecordForSpecialtyToSupplier = 8,
        /// <summary>
        ///农产品收货完成通知供应商
        /// </summary>
        [Description("{0}{1}己完成收货，详情请登录系统查看！")]
        ReceiveSuccessOrderForSpecialtyToSupplier = 9,
        /// <summary>
        ///农产品预售-景区审核退款同意通知供应商 预售商品只有商家才能发起退款，让景区审核
        /// </summary>
        [Description("{0}{1}的退款申请己同意，退款将及时返还给买方，详情请登录系统查看！")]
        RefundSuccessPreSaleForSpecialtyToSupplier = 10,
        /// <summary>
        ///农产品预售-景区审核退款拒绝通知供应商 预售商品只有商家才能发起退款，让景区审核
        /// </summary>
        [Description("{0}{1}的退款申请己被景区运营方拒绝，详情请登录系统查看！")]
        RefundFailPreSaleForSpecialtyToSupplier = 11,

        /// <summary>
        ///农产品退款同意通知C端会员 
        /// </summary>
        [Description("{0}{1}的退款己同意，支付的款项将原路退回，详情请登录系统查看！")]
        RefundSuccessToCustomer = 12,

        /// <summary>
        ///农产品退款失败通知C端会员 
        /// </summary>
        [Description("{0}{1}的退款己被供货方拒绝，详情请登录系统查看！")]
        RefundFailToCustomer = 13,
        /// <summary>
        ///农产品支付尾款通知C端会员
        /// </summary>
        [Description("{0}{1}农产品己上市，请您在{2}小时内及时支付尾款，以免影响及时发货！")]
        PayRetainageOrderToCustomer = 14,
        /// <summary>
        ///农产品发货通知C端会员
        /// </summary>
        [Description("{0}{1}己发货，请注意查收！")]
        DeliverGoodsOrderToCustomer = 15,

        /// <summary>
        ///财务管理提现审核同意通知供应商
        /// </summary>
        [Description("{0}您的提现申请己同意，请注意查收款项！")]
        WithdrawSuccessToSupplier = 16,
        /// <summary>
        ///财务管理提现审核拒绝通知供应商
        /// </summary>
        [Description("{0}您的提现申请己被拒绝，如有问题请咨询景区运营方，详情请登录系统查看！")]
        WithdrawFailToSupplier = 17,
        /// <summary>
        ///验证码
        /// </summary>
        [Description("{0}您的验证码为{1}，请于10分钟内正确输入，如非本人操作，请忽略此短信。")]
        VerificationCode = 18,
    }
    /// <summary>
    /// 短信网关
    /// </summary>
    public enum GatewayPlatformEnum
    {
        [Description("秒嘀")]
        Miaodi = 1,
    }
    public enum SMSStatus
    {
        [Description("未发送")]
        UnSend = 1,
        [Description("已发送")]
        Send = 1,
    }
}
