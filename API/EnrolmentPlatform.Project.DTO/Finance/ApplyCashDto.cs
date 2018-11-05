using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Finance;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Finance
{
    public class ApplyCashDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 账户名
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CashApplyStatusEnum Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(Status);
            }
        }


        /// <summary>
        ///提交时间
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 提交用户
        /// </summary>
        public string CreatorAccount { get; set; }

        /// <summary>
        /// 企业名称（供应商名称）
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public EnterpriceTypeEnum UserType { get; set; }

        /// <summary>
        /// 用户类型 名称
        /// </summary>
        public string UserTypeName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(UserType);
            }
        }

    }

    /// <summary>
    ///  提现申请 DTO
    /// </summary>
    public class AddApplyCashDto
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public Guid BankId { get; set; }

        /// <summary>
        ///  提现金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///  提现密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 申请人名称
        /// </summary>
        public string CreatorAccount { get; set; }
    }


    public class ApplyCashDetailDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string CreatorAccount { get; set; }
        /// <summary>
        ///  申请提现时间
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CashApplyStatusEnum Status { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { get; set; }


        /// <summary>
        /// 原因
        /// </summary>
        public string OptionReason { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 操作日志 
        /// </summary>
        public List<ApplyCashOperationLogDto> OperationLog { get; set; }

    }
    /// <summary>
    ///  提现操作日志
    /// </summary>
    public class ApplyCashOperationLogDto
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperationContent { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
    }

    /// <summary>
    ///  审核提现申请 
    /// </summary>
    public class AuditApplyCashDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public List<Guid> Ids { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public CashApplyStatusEnum Status { get; set; }

        /// <summary>
        ///  原因
        /// </summary>
        public string OptionReason { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

    }




}
