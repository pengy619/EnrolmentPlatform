using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    public class SaveTicketOrderDTO
    {
        [DataMember]
        /// <summary>
        /// 下单人ID
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 下单方
        /// </summary>
        [DataMember]
        public int CreateForSystem { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
        ///// <summary>
        ///// 班期ID
        ///// </summary>
        //[DataMember]
        //public Guid ScheduleId { get; set; }
        /// <summary>
        /// 班期
        /// </summary>
        [DataMember]
        public DateTime Schedule { get; set; }
        /// <summary>
        /// 游客集合
        /// </summary>
        [DataMember]
        public List<Tourist_SaveOrder> TouristList { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [DataMember]
        public int OrderSource { get; set; }
    }
    [Serializable]
    [DataContract]
    public class Tourist_SaveOrder
    {
        /// <summary>
        /// 游客姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string Sex { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        [DataMember]
        public int CredentialsType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        [DataMember]
        public string Credentials { get; set; }
        /// <summary>
        /// 是否为取票人
        /// </summary>
        [DataMember]
        public bool IsTicket { get; set; }
    }


}
