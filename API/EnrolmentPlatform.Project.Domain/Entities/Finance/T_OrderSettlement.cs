using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Finance
{

    [Table("T_OrderSettlement")]
    [Serializable]
    [DataContract]
    public class T_OrderSettlement : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        [DataMember]
        public Guid EnterpriseId { get; set; }
        /// <summary>
        ///结算编号
        /// </summary>
        [DataMember]
        public string SettlementNo { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        [DataMember]
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 结算总金额
        /// </summary>
        [DataMember]
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        [DataMember]
        public decimal TotalOrderAmount { get; set; }
        /// <summary>
        /// 状态 【1：待结算】【2：已结算】
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}
