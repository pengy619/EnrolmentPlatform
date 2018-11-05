using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Finance;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    /// <summary>
    ///   银行信息
    /// </summary>
    public class BankCardDto
    {
        /// <summary>
        /// 
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// 企业ID
        /// </summary> 
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 账户/法人名称
        /// </summary> 
        public string CardName { get; set; }
        /// <summary>
        /// 证件号
        /// </summary> 
        public string IDNumber { get; set; }
        /// <summary>
        /// 银行卡类型【1：企业】【2：个人】
        /// </summary> 
        public BankCardTypeEnum CardClassify { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary> 
        public string CardNumber { get; set; }
        /// <summary>
        /// 开户行
        /// </summary> 
        public BankNameEnum Bank { get; set; }
        /// <summary>
        /// 地址ID
        /// </summary> 
        public Guid AddressId { get; set; }
        /// <summary>
        /// 分行地址
        /// </summary> 
        public string SubBankAddress { get; set; }
        /// <summary>
        /// 手机号
        /// </summary> 
        public string Phone { get; set; }
        /// <summary>
        /// 状态
        /// </summary> 
        public int Status { get; set; }

       /// <summary>
       /// 创建用户ID
       /// </summary>
        public Guid CreatorUserId { get; set; }
    
        /// <summary>
        /// 创建用户名称
        /// </summary>
        public string CreatorAccount { get; set; }
        /// <summary>
        /// 修改用户ID
        /// </summary>
        public Guid LastModifyUserId { get; set; }


        
    }
}
