using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    public class BaseOrderOperationDTO: BasePostOperation
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        [DataMember]
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        [DataMember]
        public Guid OrderId { get; set; }
    }
}
