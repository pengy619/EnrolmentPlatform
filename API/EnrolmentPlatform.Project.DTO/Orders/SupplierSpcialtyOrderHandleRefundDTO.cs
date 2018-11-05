using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 供应商处理C端用户的退款
    /// </summary>
    [Serializable]
    [DataContract]
    public class SupplierSpcialtyOrderHandleRefundDTO : BaseOrderOperationDTO
    {
        /// <summary>
        /// 是否同意
        /// </summary>
        [DataMember]
        public bool IsAgree { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        [DataMember]
        public decimal RefundAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }

    }
}
