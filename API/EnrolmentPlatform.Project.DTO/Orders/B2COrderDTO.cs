using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    public class SearchParamForB2COrder : GridDataRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        [DataMember]
        public int OrderClassify { get; set; }
        /// <summary>
        /// 是否是预售
        /// </summary>
        [DataMember]
        public bool IsPresale { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [DataMember]
        public Guid CreateUserId { get; set; }
    }
    public class B2COrderInfo
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int TotalQuantity { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 修改后的金额
        /// </summary>
        [DataMember]
        public decimal UpdateTotalAmount { get; set; }
        /// <summary>
        /// 实际支付
        /// </summary>
        [DataMember]
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        [DataMember]
        public int OrderClassify { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        [DataMember]
        public string OrderClassifyCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((OrderClassifyEnum)this.OrderClassify);
            }
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public string OrderStatusCH
        {
            get
            {
                string status = string.Empty;
                if (this.OrderClassify == (int)OrderClassifyEnum.Specialty)
                {
                    return EnumDescriptionHelper.GetDescription((OrderStatusForSpecialtyEnum)this.OrderStatus);
                }
                else if (this.OrderClassify == (int)OrderClassifyEnum.Ticket)
                {
                    return EnumDescriptionHelper.GetDescription((OrderStatusForTicketEnum)this.OrderStatus);
                }
                return status;
            }
        }
        /// <summary>
        /// 取消方
        /// </summary>
        [DataMember]
        public int CancelForSystem { get; set; }
        /// <summary>
        /// 出行日期-门票
        /// </summary>
        [DataMember]

        public DateTime PlayDay { get; set; }
        /// <summary>
        /// 是否可退
        /// </summary>
        [DataMember]

        public bool IsRefund
        {
            get
            {
                bool res = true;
                string status = string.Empty;
                //至于已支付的订单才显示退款
                if (!(this.PayStatus == (int)OrderPayStatusEnum.Paid))
                {
                    return false;
                }
                if (this.OrderClassify == (int)OrderClassifyEnum.Specialty)
                {
                    //预售不可退
                    if (this.SalesModel == (int)ProductSaleModelEnum.Presale)
                    {
                        res = false;
                    }
                    //退款中和已完成不可退
                    if (this.OrderStatus == (int)OrderStatusForSpecialtyEnum.Received || this.OrderStatus == (int)OrderStatusForSpecialtyEnum.Completed || this.OrderStatus == (int)OrderStatusForSpecialtyEnum.Refunding|| this.OrderStatus == (int)OrderStatusForSpecialtyEnum.Refunded)
                    {
                        res = false;
                    }
                }
                else if (this.OrderClassify == (int)OrderClassifyEnum.Ticket)
                {
                    //已完成的和退款中的不能退
                    if (this.OrderStatus == (int)OrderStatusForTicketEnum.Completed || this.OrderStatus == (int)OrderStatusForTicketEnum.Refunding|| this.OrderStatus == (int)OrderStatusForTicketEnum.Refunded)
                    {
                        res = false;
                    }
                    //退票规则
                    if (this.RefundRule == (int)RefundPriceEnum.No)
                    {
                        res = false;
                    }
                    else
                    {
                        //游玩前
                        if (this.IsBefore)
                        {
                            if (this.PlayDay.AddDays(-(this.RefundDay - 1)) <= DateTime.Today)
                            {
                                res = false;

                            }
                        }
                        else
                        {
                            if (this.PlayDay.AddDays(this.RefundDay + 1) <= DateTime.Today)
                            {
                                res = false;
                            }
                        }
                    }
                }
                return res;
            }
        }
        /// <summary>
        /// 门票退票规则
        /// </summary>
        public int RefundRule { get; set; }
        /// <summary>
        /// 是否是游玩日期前 true 前false后
        /// </summary>

        public bool IsBefore { get; set; }
        /// <summary>
        /// 可退天数
        /// </summary>
        public int RefundDay { get; set; }
        /// <summary>
        /// 销售模式
        /// </summary>
        [DataMember]
        public int SalesModel { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        [DataMember]
        public int PayStatus { get; set; }
        /// <summary>
        /// 支付剩余时间
        /// </summary>
        [DataMember]

        public string PaySurplusTime { get; set; }
        /// <summary>
        /// 待付尾款时间
        /// </summary>
        [DataMember]

        public DateTime? PayRetainageTime { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        [DataMember]
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 创建方
        /// </summary>
        [DataMember]
        public int CreateForSystem { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int OrderSource { get; set; }

        /// <summary>
        /// 是否有退换货记录
        /// </summary>
        [DataMember]
        public bool IsDisplayRefundableButton { get; set; }
    }
}
