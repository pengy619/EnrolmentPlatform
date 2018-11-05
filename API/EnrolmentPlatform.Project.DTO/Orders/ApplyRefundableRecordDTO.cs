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
    public class ApplyRefundableRecordDTO: BasePostOperation
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }

        /// <summary>
        /// 理由
        /// </summary>
        [DataMember]
        public string Reason { get; set; }

        /// <summary>
        /// 详细理由
        /// </summary>
        [DataMember]
        public string ReasonDetail { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        [DataMember]
        public int ProductQuantity { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [DataMember]
        public int OptionClassify { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public List<string> FileList { get; set; }

        /// <summary>
        /// 订单项ID
        /// </summary>
        [DataMember]
        public Guid OrderItemId { get; set; }

    }
}
