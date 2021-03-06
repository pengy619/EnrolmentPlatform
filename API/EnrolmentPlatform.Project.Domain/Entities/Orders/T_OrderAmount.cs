﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Orders
{
    /// <summary>
    /// 订单金额
    /// </summary>
    [Serializable]
    [Table("T_OrderAmount")]
    [DataContract]
    public class T_OrderAmount:Entity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { set; get; }

        /// <summary>
        /// 支付发起方（1：招生机构，2：渠道中心）
        /// </summary>
        [DataMember]
        public int PaymentSource { set; get; }

        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal TotalAmount { set; get; }

        /// <summary>
        /// 已缴金额
        /// </summary>
        [DataMember]
        public decimal PayedAmount { set; get; }

        /// <summary>
        /// 待审核金额
        /// </summary>
        [DataMember]
        public decimal ApprovalAmount { set; get; }
    }
}
