using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    [Serializable]
    [DataContract]
    public class TicketOrderVerificationSearchParamDTO: GridDataRequest
    {
        [DataMember]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 核销状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        [DataMember]
        public int SupplierType { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 激活码
        /// </summary>
        [DataMember]
        public string ActivationCode { get; set; }

        /// <summary>
        /// 唯一取票码
        /// </summary>
        [DataMember]
        public string UnifiedCheckCode { get; set; }

        /// <summary>
        /// 票号
        /// </summary>
        [DataMember]
        public string TicketToken { get; set; }
    }
    [Serializable]
    [DataContract]
    public class TicketOrderVerificationInfo
    {
        /// <summary>
        /// 核销id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [DataMember]
        public string ProductNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        [DataMember]
        public int SupplierType { get; set; }
        [DataMember]
        /// <summary>
        /// 供应商类型
        /// </summary>
        public string SupplierTypeCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((SupplierTypeEnum)this.SupplierType);
            }
        }
        [DataMember]
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 激活码
        /// </summary>
        [DataMember]
        public string ActivationCode { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        [DataMember]
        public DateTime ExpiryDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 激活码中文
        /// </summary>
        [DataMember]
        public string StatusCH
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((TicketVerificationStatusEnum)this.Status);
            }
        }
        /// <summary>
        /// 核销时间
        /// </summary>
        [DataMember]
        public DateTime VerificationDate { get; set; }
        [DataMember]
        public string VerificationDateCH
        {
            get
            {
                string res = string.Empty;
                if (this.Status==(int)TicketVerificationStatusEnum.Verificated)
                {
                    res=this.LastModifyTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return res;
            }
        }
        /// <summary>
        /// 核销方式
        /// </summary>
        [DataMember]
        public int Pattern { get; set; }
        /// <summary>
        /// 核销方式中文
        /// </summary>
        [DataMember]
        public string PatternCH {
            get
            {
                if (this.Status == (int)TicketVerificationStatusEnum.Verificated)
                {
                    return EnumDescriptionHelper.GetDescription((VerificationModeEnum)this.Pattern);
                }
                else
                {
                    return string.Empty;
                }
                
            }
        }
        [DataMember]
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 唯一取票码
        /// </summary>
        [DataMember]
        public string UnifiedCheckCode { get; set; }

        /// <summary>
        /// 票号
        /// </summary>
        [DataMember]
        public string TicketToken { get; set; }

        /// <summary>
        /// 景点项目名称
        /// </summary>
        [DataMember]
        public string ScenicProjectName { get; set; }
    }
}
