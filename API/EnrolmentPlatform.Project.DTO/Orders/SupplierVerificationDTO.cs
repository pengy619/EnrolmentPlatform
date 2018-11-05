using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 供应商核销DTO
    /// </summary>
    [Serializable]
    [DataContract]
    public class SupplierVerificationDTO: BaseOrderOperationDTO
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 核销方式
        /// </summary>
        [DataMember]
        public int VerificationMode { get; set; }
    }
    /// <summary>
    /// 供应商设置失效DTO
    /// </summary>
    [Serializable]
    [DataContract]
    public class SupplierInvalidDTO : BaseOrderOperationDTO
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
    }
}
