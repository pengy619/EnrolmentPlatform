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
    public class SupplierCateringOrderRefundDTO : BaseOrderOperationDTO
    {
        /// <summary>
        /// 退款理由
        /// </summary>
        [DataMember]
        public string RefundReason { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}
