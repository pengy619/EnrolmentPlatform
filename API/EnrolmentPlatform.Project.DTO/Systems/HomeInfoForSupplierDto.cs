using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{ 
    /// <summary>
    /// 首页静态统计
    /// </summary>
    [Serializable]
    [DataContract]
    public class HomeInfoForSupplierDto
    {
        /// <summary>
        /// 总待办事数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForAll { get; set; }
        /// <summary>
        /// 门票待退款数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForTicketOrderForRefund { get; set; }

        /// <summary>
        /// 农场品票待退款数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForSpecialtyOrderForRefund { get; set; }
        /// <summary>
        /// 餐饮待退款数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForCateringOrderForRefund { get; set; }

        /// <summary>
        /// 待发货
        /// </summary>
        [DataMember]
        public int CountForSupplierForOrderForShip { get; set; }
        /// <summary>
        /// 待退换货
        /// </summary>
        [DataMember]
        public int CountForSupplierForOrderForExchange { get; set; } 
        /// <summary>
        /// 农产品待审核数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForProductForSpecialty { get; set; }
        /// <summary>
        /// 票务待审核数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForProductForTicket { get; set; }
        /// <summary>
        /// 票务待审核数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForProductForCatering { get; set; }
        /// <summary>
        /// 提现待审核数量
        /// </summary>
        [DataMember]
        public int CountForSupplierForFinaceForWithdraw { get; set; } 
    }
     
    /// <summary>
    /// 首页动态统计
    /// </summary>
    [Serializable]
    [DataContract]
    public class HomeInfoForAdminDto
    { 
        /// <summary>
        /// 新增订单数
        /// </summary>
        [DataMember]
        public int CountForOrderForAdd { get; set; }
        /// <summary>
        /// 销售额
        /// </summary>
        [DataMember]
        public decimal PriceForOrderForAddForSale { get; set; } 
        /// <summary>
        /// 退款订单数
        /// </summary>
        [DataMember]
        public int CountForOrderForCancel { get; set; }
        /// <summary>
        /// 退款额
        /// </summary>
        [DataMember]
        public decimal PriceForOrderForCancel { get; set; } 
        /// <summary>
        /// 新增会员数
        /// </summary>
        [DataMember]
        public int CountForUser { get; set; }
        /// <summary>
        /// 总会员数
        /// </summary>
        [DataMember]
        public int CountForUserAll { get; set; }
        /// <summary>
        /// 新上架商品数
        /// </summary>
        [DataMember]
        public int CountForProductForAdd { get; set; }
        /// <summary>
        /// 已上架商品总数
        /// </summary>
        [DataMember]
        public int CountForProductAll { get; set; }
        /// <summary>
        /// 新增供应商数
        /// </summary> 
        [DataMember]
        public int CountForSupplierUser { get; set; }
        /// <summary>
        /// 总供应商数
        /// </summary>
        [DataMember]
        public int CountForSupplierUserAll { get; set; }
    }
}
