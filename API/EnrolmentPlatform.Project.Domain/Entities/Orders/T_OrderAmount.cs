﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Orders
{
    [Serializable]
    [Table("T_Order")]
    [DataContract]
    public class T_OrderAmount:Entity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 支付发起方（1：机构，2：渠道）
        /// </summary>
        public int PaymentSource { set; get; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { set; get; }

        /// <summary>
        /// 已缴金额
        /// </summary>
        public decimal PayedAmount { set; get; }

        /// <summary>
        /// 未缴金额
        /// </summary>
        public decimal UnPayedAmount { set; get; }

        /// <summary>
        /// 待审核金额
        /// </summary>
        public decimal ApprovalAmount { set; get; }
    }
}
