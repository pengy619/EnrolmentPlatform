using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_Enterprise")]
    [DataContract]
    public class T_Enterprise : Entity
    {
        /// <summary>
        /// 企业名称
        /// </summary> 
        [DataMember]
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 业务类型【1：农户】【2：商户】
        /// </summary> 
        [DataMember]
        public int BusinessType { get; set; }
        /// <summary>
        /// 业务范围【1：游乐项目】【2：酒店/民宿】【3：农场品】【4：土特产】
        /// </summary> 
        [DataMember]
        public string BusinessRang { get; set; }
        /// <summary>
        /// 税号
        /// </summary> 
        [DataMember]
        public string TaxNo { get; set; }
        /// <summary>
        /// 传真
        /// </summary> 
        [DataMember]
        public string Fax { get; set; }
        /// <summary>
        /// 联系人
        /// </summary> 
        [DataMember]
        public string Contact { get; set; }
        /// <summary>
        /// 手机号
        /// </summary> 
        [DataMember]
        public string Phone { get; set; }
        /// <summary>
        /// 地址ID
        /// </summary> 
        [DataMember]
        public Guid AddressId { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary> 
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 结算周期【1：及时】【2：周结】【3：月结】
        /// </summary> 
        [DataMember]
        public int SettlementCycle { get; set; }
        /// <summary>
        /// 费率
        /// </summary> 
        [DataMember]
        public decimal Rate { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public Decimal DepositAmount { set; get; }

        /// <summary>
        /// 类型【3：供应商】【4：分销商】
        /// </summary> 
        [DataMember]
        public int Classify { get; set; }
        /// <summary>
        /// 提现密码
        /// </summary> 
        [DataMember]
        public string CashPassWord { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 状态【1：未启用】【2：启用】
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 法人
        /// </summary> 
        public string LegalPerson { get; set; }
        /// <summary>
        /// 余额
        /// </summary> 
        public decimal Balance { get; set; }

        /// <summary>
        /// 上次结算时间  
        /// </summary>
        public DateTime? LastSettlementDate { get; set; }

        /// <summary>
        /// 下次结算时间
        /// </summary>
        public DateTime? NextSettlementDate { get; set; }

        /// <summary>
        /// 营业执照到期时间 
        /// </summary>
        public DateTime? BusinessEndDate { get; set; }

        /// <summary>
        /// 供应商类型  1：自营供应商，2：外部供应商
        /// </summary>
        public int SupplierType { get; set; }

        /// <summary>
        /// 营业执照编号
        /// </summary>
        public string LicenseNo { set; get; }

    }
}
