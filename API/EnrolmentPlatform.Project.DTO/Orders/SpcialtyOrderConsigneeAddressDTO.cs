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
    public class SpcialtyOrderConsigneeAddressDTO: BaseOrderOperationDTO
    {
        /// <summary>
        /// 收货人
        /// </summary>
        [DataMember]
        public string Consignee { get; set; }
        /// <summary>
        /// 收货人地址
        /// </summary>
        [DataMember]
        public string ConsigneeAddress { get; set; }
        /// <summary>
        /// 收货人手机
        /// </summary>
        [DataMember]
        public string ConsigneePhone { get; set; }
    }
}
