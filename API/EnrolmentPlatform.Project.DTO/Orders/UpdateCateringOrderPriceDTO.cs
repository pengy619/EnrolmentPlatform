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
    public class UpdateCateringOrderPriceDTO: BaseOrderOperationDTO
    {
        /// <summary>
        /// 修改总金额
        /// </summary>
        [DataMember]
        public decimal UpdateTotalAmount { get; set; }
    }
}
