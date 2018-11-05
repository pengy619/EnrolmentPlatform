using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Accounts
{
    /// <summary>
    /// 企业信息添加DTO
    /// </summary>
    public class EnterpriseAddDto
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary> 
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 类型【1景区运营端】【2用户端】【3供应商端】【4分销商端】 SystemTypeEnum 枚举
        /// </summary> 
        public EnterpriceTypeEnum Classify { get; set; }


        /// <summary>
        /// 供应商类型    1：自营供应商，2：外部供应商
        /// </summary>
        public SupplierTypeEnum SupplierType { get; set; }

        /// <summary>
        /// 业务类型【1：农户】【2：商户】
        /// </summary> 
        public EnterpriseBusinessTypeEnum? BusinessType { get; set; }

        /// <summary>
        /// 业务范围【1：游乐项目】【2：酒店/民宿】【3：农场品】【4：土特产】
        /// </summary> 
        public List<string> BusinessRang { get; set; }

        /// <summary>
        /// 联系人
        /// </summary> 
        public string Contact { get; set; }

        /// <summary>
        /// 手机号
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// 地址ID
        /// </summary> 
        public Guid AddressId { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary> 
        public string Address { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary> 
        public string UserAccount { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary> 
        public string UserPwd { get; set; }

        /// <summary>
        /// 结算周期【1：及时】【2：周结】【3：月结】
        /// </summary>
        public SettlementCycleEnum SettlementCycle { get; set; }

        /// <summary>
        /// 费率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public decimal DepositAmount { set; get; }

        /// <summary>
        /// 营业执照编号
        /// </summary>
        public string LicenseNo { set; get; }

        /// <summary>
        ///  营业执照 到期时间
        /// </summary>·
        public DateTime? BusinessEndDate { set; get; }

        /// <summary>
        /// 备注
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        /// 营业执照照片
        /// </summary>
        public string BusinessLicenseUrl { set; get; }

        /// <summary>
        /// 身份证正面照片
        /// </summary>
        public string IDCardUpwardsUrl { set; get; }

        /// <summary>
        /// 身份证正面照片
        /// </summary>
        public string IDCardReverseUrl { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid CurUserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string CurUserAccount { set; get; }
    }

    /// <summary>
    /// 供应商端-企业信息获取DTO
    /// </summary>
    public class SupplierEnterpriseGetDto
    {
        /// <summary>
        /// 企业名称
        /// </summary> 
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 业务类型【1：农户】【2：商户】
        /// </summary> 
        public string BusinessType { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        public string SupplierType { get; set; }
        /// <summary>
        /// 业务范围【1：游乐项目】【2：酒店/民宿】【3：农场品】【4：土特产】
        /// </summary> 
        public string BusinessRang { get; set; }

        /// <summary>
        /// 联系人
        /// </summary> 
        public string Contact { get; set; }

        /// <summary>
        /// 手机号
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// 完整地址
        /// </summary> 
        public string FullAddress { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary> 
        public string UserAccount { get; set; }

        /// <summary>
        /// 结算周期【1：及时】【2：周结】【3：月结】
        /// </summary>
        public string SettlementCycle { get; set; }

        /// <summary>
        /// 费率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public decimal DepositAmount { set; get; }

        /// <summary>
        /// 营业执照照片
        /// </summary>
        public string BusinessLicenseUrl { set; get; }

        /// <summary>
        /// 身份证正面照片
        /// </summary>
        public string IDCardUpwardsUrl { set; get; }

        /// <summary>
        /// 身份证正面照片
        /// </summary>
        public string IDCardReverseUrl { set; get; }
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid Id { get; set; }
    }


    /// <summary>
    /// 设置提现密码DTO 
    /// </summary>
    public class SetCashPasswordDto
    {
        /// <summary>
        /// 企业ID
        /// </summary>
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 操作人Id
        /// </summary>
        public Guid OperatorId { get; set; }
    }

    /// <summary>
    /// 供应商列表Dto
    /// </summary>
    public class SupplierListDto
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 业务类型【1：农户】【2：商户】
        /// </summary> 
        public int BusinessType { get; set; }

        public string BusinessTypeStr
        {
            get
            {
                if (BusinessType == 0)
                    return "自营";
                else
                    return EnumDescriptionHelper.GetDescription((EnterpriseBusinessTypeEnum)BusinessType);
            }
        }

        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginAccount { get; set; }

        /// <summary>
        /// 联系人
        /// </summary> 
        public string Contact { get; set; }

        /// <summary>
        /// 结算周期【1：及时】【2：周结】【3：月结】
        /// </summary>
        public int SettlementCycle { get; set; }

        public string SettlementCycleStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((SettlementCycleEnum)SettlementCycle);
            }
        }

        /// <summary>
        /// 状态【1：未启用】【2：启用】
        /// </summary>
        public int Status { get; set; }

        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusBaseEnum)Status);
            }
        }
    }

    /// <summary>
    /// 供应商查询Dto
    /// </summary>
    public class SupplierSearchDto : GridDataRequest
    {
        /// <summary>
        /// 启用状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginAccount { get; set; }
    }

    /// <summary>
    /// 更新企业状态 
    /// </summary>
    public class UpdateEnterpriseStatusDto
    {
        /// <summary>
        ///  企业ID集合 
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator { get; set; }

    }


    public class DeleteEnterpriseDto
    {
        /// <summary>
        ///  企业ID集合 
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator { get; set; }

    }

}
