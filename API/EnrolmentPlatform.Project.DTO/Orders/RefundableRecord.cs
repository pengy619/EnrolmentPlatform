using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    public class RefundableRecordParam : GridDataRequest
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 退货编号
        /// </summary>
        public string RefundableNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid AccountId { get; set; }
    }
    [Serializable]
    [DataContract]
    public class RefundableRecord
    {
        /// <summary>
        /// 退换货Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 退换货编号
        /// </summary>
        [DataMember]
        public string RefundableNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreatorTime { get; set; }
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
        /// 产品数量
        /// </summary>
        [DataMember]
        public int ProductQuantity { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 状态中文
        /// </summary>
        [DataMember]
        public string StatusCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((RefundableStatusEnum)this.Status);
            }
        }
        /// <summary>
        /// 退换货操作类型
        /// </summary>
        [DataMember]
        public int OptionClassify { get; set; }
        /// <summary>
        /// 退换货操作类型 中文
        /// </summary>
        [DataMember]
        public string OptionClassifyCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((RefundableOptionClassifyEnum)this.OptionClassify);
            }
        }

        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public string Photo { get; set; }
    }

    /// <summary>
    /// H5退换货
    /// </summary>
    public class RefundableRecordParam_H5 : GridDataRequest
    {
        public Guid AccountId { get; set; }

        public int OptionClassify { get; set; }

        public string RefundableNo { get; set; }

        public List<RefundableRecord> RefundableRecordList { get; set; }
    }

}
