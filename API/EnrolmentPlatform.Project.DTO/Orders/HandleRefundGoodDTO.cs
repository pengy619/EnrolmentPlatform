using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 供应商处理退货
    /// </summary>
    [Serializable]
    [DataContract]
    public class HandleRefundGoodDTO: BasePostOperation
    {
        /// <summary>
        /// 处理退款的订单ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
    }
}
