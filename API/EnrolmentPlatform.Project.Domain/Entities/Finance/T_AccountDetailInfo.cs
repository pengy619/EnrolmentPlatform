using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_AccountDetailInfo")]
    [DataContract]
    public class T_AccountDetailInfo : Entity
    {
        /// <summary>
        /// 企业ID
        /// </summary> 
        [DataMember]
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 金额
        /// </summary> 
        [DataMember]
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易类型 1：订单结算 ，2：提现
        /// </summary> 
        [DataMember]
        public int TransactionClassify { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary> 
        [DataMember]
        public string TranscationNo { get; set; }
         /// <summary>
         /// 交易前金额
         /// </summary>
        public decimal TranscationBeforeAmount { get; set; }
        /// <summary>
        /// 交易后金额
        /// </summary>
        public decimal TranscationAfterAmount { get; set; }
        /// <summary>
        /// 订单编号/提现申请ID
        /// </summary> 
        [DataMember]
        public string OrderNo { get; set; }
    }
}
