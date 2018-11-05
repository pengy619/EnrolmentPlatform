using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Finance;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    public class BankCardListDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  账户/法人名称
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 企业类型
        /// </summary>
        public EnterpriceTypeEnum EnterpriceType { get; set; }

        /// <summary>
        /// 银行卡类型
        /// </summary>
        public BankCardTypeEnum BankCardType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 企业类型 名称
        /// </summary>
        public string EnterpriceTypeName
        {
            get { return Infrastructure.EnumHelper.EnumDescriptionHelper.GetDescription(EnterpriceType); }
        }

        /// <summary>
        /// 银行卡类型 名称
        /// </summary>
        public string BankCardTypeName
        {
            get { return Infrastructure.EnumHelper.EnumDescriptionHelper.GetDescription(BankCardType); }
        }

    }

    /// <summary>
    ///  银行卡 分页请求类
    /// </summary>
    public class BankCardListRequestDto : GridDataRequest
    {

        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        ///  账户/法人名称
        /// </summary>
        public string CardName { get; set; }

    }

    /// <summary>
    /// 银行卡详情 Dto
    /// </summary>
    public class BankCardDetailDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  账户/法人名称
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 企业类型
        /// </summary>
        public EnterpriceTypeEnum EnterpriceType { get; set; }

        /// <summary>
        /// 银行卡类型
        /// </summary>
        public BankCardTypeEnum BankCardType { get; set; }

        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorAccount { get; set; }
    }

}

