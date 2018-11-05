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
    public class SpcialtyOrderSendGoodDTO : BaseOrderOperationDTO
    {
        /// <summary>
        /// 快递公司
        /// </summary>
        [DataMember]
        public string ExpressCompany { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        [DataMember]
        public string WaybillNumber { get; set; }
    }
}
