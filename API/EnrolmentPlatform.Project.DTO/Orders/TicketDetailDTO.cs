using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 门票详细DTO
    /// </summary>
    [Serializable]
    [DataContract]
    public class TicketDetailDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 取票码
        /// </summary>
        [DataMember]
        public string TicketToken { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 游玩日期
        /// </summary>
        [DataMember]
        public DateTime PlayDay { get; set; }

        /// <summary>
        /// 门票核销信息
        /// </summary>
        [DataMember]
        public List<VerificationForTicket_TicketDetail> TicketVerification { get; set; }



    }
    [Serializable]
    [DataContract]
    /// <summary>
    /// 门票核销项目
    /// </summary>
    public class VerificationForTicket_TicketDetail
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [DataMember]
        public string ScenicProjectName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
    }
}
