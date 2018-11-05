using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    public class SettlementsDetailsDto
    {
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
        /// 结算 状态
        /// </summary>
        public OrderSettlementStatusEnum Status { get; set; }

        /// <summary>
        /// 申请时间  
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string CreatorAccount { get; set; }
            

        /// <summary>
        /// 结算周期 
        /// </summary>
        public string SettlementCycle { get; set; }

        /// <summary>
        /// 结算单 订单列表 
        /// </summary>
        public List<SettlementsOrderInfoDto> SettlementsOrderInfoDtos { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SettlementsOrderInfoDto
    {
        /// <summary>
        ///  订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        ///  订单类型
        /// </summary>
        public OrderClassifyEnum Classify { get; set; }

        /// <summary>
        /// 订单类型 名称
        /// </summary>
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

        /// <summary>
        /// 订单状态 名称
        /// </summary>
        public string OrderStatusName
        {
            get
            {
                if (Classify == OrderClassifyEnum.Specialty)
                    return EnumDescriptionHelper.GetDescription((OrderStatusForSpecialtyEnum)OrderStatus);
                else
                    return EnumDescriptionHelper.GetDescription((OrderStatusForTicketEnum)OrderStatus);
            }
        }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 结算金额 
        /// </summary>
        public decimal SettlementAmount { get; set; }

        public DateTime CreatorTime { get; set; }
    }

}
