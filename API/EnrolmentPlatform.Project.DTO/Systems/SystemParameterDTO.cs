using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 系统参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class SystemParameterDTO:BasePostOperation
    {
        [DataMember]
        public string B2CSpecialtyCancelTime { get; set; }
        [DataMember]
        public string B2CTicketCancelTime { get; set; }
        [DataMember]
        public string TouristsCenterTicketCancelTime { get; set; }
        [DataMember]
        public string SpecialtyAutoReceiptTime { get; set; }
        [DataMember]
        public string IsNeedAdultBySelfSupplier { get; set; }
        [DataMember]
        public string TikcetAutoComplete { get; set; }
        [DataMember]
        public string PayRetainageAutoCancel { get; set; }
        [DataMember]
        public string CateringOrderAutoCancel { get; set; }
    }
}
