using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_OrderSettlementInfo")]
    [DataContract]
    public class T_OrderSettlementInfo : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary> 
        [DataMember]
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 结算编号
        /// </summary> 
        [DataMember]
        public string SettlementNo { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary> 
        [DataMember]
        public string OrderNo { get; set; }

        /// 订单名称
        /// </summary> 
        [DataMember]
        public string OrderName { get; set; }
        /// <summary>
        /// 订单类型【1：农产品】【2：门票】
        /// </summary> 
        [DataMember]
        public int OrderClassify { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary> 
        [DataMember]
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 结算总金额
        /// </summary> 
        [DataMember]
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary> 
        [DataMember]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary> 
        [DataMember]
        public string SettlementCycle { get; set; }
        /// <summary>
        /// 状态【1：待结算】【2：已结算】
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
