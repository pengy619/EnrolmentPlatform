using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Finance;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    /// <summary>
    /// 待结算订单Dto
    /// </summary>
    public class PendingOrderInfoDto
    {
        /// <summary>
        ///  订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        ///  订单类型
        /// </summary>
        public OrderClassifyEnum Classify { get; set; }


        public string ClassifyName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(Classify);
            }
        }



        /// <summary>
        /// 产品信息
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }

        public string OrderStatusName
        {
            get
            {
                if (Classify == OrderClassifyEnum.Specialty)
                {
                    var tempEnum = (OrderStatusForSpecialtyEnum)OrderStatus;
                    return EnumDescriptionHelper.GetDescription(tempEnum);
                }
                else
                {
                    var tempEnum = (OrderStatusForTicketEnum)OrderStatus;
                    return EnumDescriptionHelper.GetDescription(tempEnum);
                }               
            }
        }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 待结算金额 
        /// </summary>
        public decimal PendingAmount { get; set; }

        public DateTime CreatorTime { get; set; }

    }


    /// <summary>
    ///  结算单 DTO
    /// </summary>
    public class OrderSettlementDto
    {

        public Guid Id { get; set; }
        /// <summary>
        ///  结算单
        /// </summary>
        public string SettlementNo { get; set; }

        /// <summary>
        ///  订单数量
        /// </summary>
        public int OrderQuantity { get; set; }

        /// <summary>
        /// 结算总金额
        /// </summary>
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalOrderAmount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public SettlementStatusEnum Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(Status);
            }
        }

        /// <summary>
        /// 申请时间  
        /// </summary>
        public DateTime CreatorTime { get; set; }
    }

    /// <summary>
    /// 待结算订单  请求类
    /// </summary>
    public class PendingOrderInfoRequestDto : GridDataRequest
    {
        /// <summary>
        ///  企业ID
        ///  无企业ID时，查询所有的企业的 景区后台
        /// </summary>
        public Guid? EnterpriseId { get; set; }
    }

    /// <summary>
    /// 结算单  请求类
    /// </summary>
    public class OrderSettlementRequestDto : GridDataRequest
    {
        /// <summary>
        /// 企业ID
        /// 无企业ID时，查询所有的企业的  景区后台
        /// </summary>
        public Guid? EnterpriseId { get; set; }
    }
}
