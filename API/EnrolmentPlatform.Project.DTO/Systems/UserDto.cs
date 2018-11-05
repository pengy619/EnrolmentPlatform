using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 用户DTO
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { set; get; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public SystemTypeEnum SystemType { set; get; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentId { set; get; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid JobID { set; get; }

        /// <summary>
        /// 是否是主账号
        /// </summary>
        public bool IsMaster { set; get; }
        /// <summary>
        /// 账号类型
        /// </summary>
        public string IsMasterStr
        {
            get
            {
                return IsMaster ? "主账号" : "子账号";
            }
        }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex { set; get; }

        /// <summary>
        /// 所属企业ID（供应商/分销商）
        /// </summary>
        public Guid EnterpriseId { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        public Guid RoleId { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 状态Int值
        /// </summary>
        public int IntStatus { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Picture { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusBaseEnum Status
        {
            get
            {
                return (StatusBaseEnum)this.IntStatus;
            }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(this.Status);
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { set; get; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public Guid CreateUserId { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateAccount { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

    }

    /// <summary>
    /// 供应商用户DTO
    /// </summary>
    public class SupplierUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { set; get; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public SystemTypeEnum SystemType { set; get; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentId { set; get; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid JobID { set; get; }

        /// <summary>
        /// 是否是主账号
        /// </summary>
        public bool IsMaster { set; get; }
        /// <summary>
        /// 账号类型
        /// </summary>
        public string IsMasterStr
        {
            get
            {
                return IsMaster ? "主账号" : "子账号";
            }
        }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string Sex { set; get; }

        /// <summary>
        /// 所属企业ID（供应商/分销商）
        /// </summary>
        public Guid EnterpriseId { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        public Guid RoleId { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 状态Int值
        /// </summary>
        public int IntStatus { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Picture { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusBaseEnum Status
        {
            get
            {
                return (StatusBaseEnum)this.IntStatus;
            }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(this.Status);
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { set; get; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public Guid CreateUserId { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateAccount { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 是否允许手持终端登录
        /// </summary>
        public bool IsAllowMobileLogin { set; get; }

        /// <summary>
        /// 允许核销的资源集合
        /// </summary>
        public List<AccountVerificationDto> AccountVerificationList { set; get; }
    }

    /// <summary>
    /// 用户DTO
    /// </summary>
    public class UserSearchDto : GridDataRequest
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { set; get; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid? RoleId { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusBaseEnum? Status { set; get; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public SystemTypeEnum SystemType { set; get; }

        /// <summary>
        /// 所属企业ID（供应商/分销商）
        /// </summary>
        public Guid EnterpriseId { set; get; }

        /// <summary>
        /// 是否是主账号
        /// </summary>
        public bool? IsMaster { set; get; }
        /// <summary>
        /// 账号类型
        /// </summary>
        public string IsMasterStr
        {
            get
            {
                return IsMaster.HasValue && IsMaster.Value ? "主账号" : "子账号";
            }
        }
    }

    /// <summary>
    /// 用户登录请求DTO
    /// </summary>
    public class UserLoginRequestDto
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { set; get; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemTypeEnum SystemType { set; get; }
    }

    /// <summary>
    /// 用户登录信息DTO
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { set; get; }

        /// <summary>
        /// 是否是主账号
        /// </summary>
        public bool IsMaster { set; get; }
        /// <summary>
        /// 账号类型
        /// </summary>
        public string IsMasterStr
        {
            get
            {
                return IsMaster ? "主账号" : "子账号";
            }
        }

        /// <summary>
        /// 所属企业ID（供应商/分销商）
        /// </summary>
        public Guid EnterpriseId { set; get; }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        public Guid RoleId { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { set; get; }
    }

    /// <summary>
    /// 景区后台用户列表DTO
    /// </summary>
    public class AdminUserListDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { set; get; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentId { set; get; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { set; get; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid JobID { set; get; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string JobName { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { set; get; }

        /// <summary>
        /// 用户角色ID
        /// </summary>
        public Guid RoleId { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 带*的手机号
        /// </summary>
        public string SercretPhone
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Phone))
                {
                    if (this.Phone.Length == 11)
                    {
                        return this.Phone.Substring(0, 3) + "*****" + this.Phone.Substring(8);
                    }
                    return this.Phone;
                }
                return "";
            }
        }

        /// <summary>
        /// 状态Int值
        /// </summary>
        public int IntStatus { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusBaseEnum Status
        {
            get
            {
                return (StatusBaseEnum)this.IntStatus;
            }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(this.Status);
            }
        }
        public bool IsMaster { get; set; }
        /// <summary>
        /// 账号类型
        /// </summary>
        public string IsMasterStr
        {
            get
            {
                return IsMaster ? "主账号" : "子账号";
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDatetime { set; get; }
    }

    /// <summary>
    /// 修改用户状态DTO
    /// </summary>
    public class ChangeUserStatusDto
    {
        /// <summary>
        /// 用户IDs
        /// </summary>
        public Guid[] UserIds { set; get; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public StatusBaseEnum UserStatus { set; get; }
    }

    /// <summary>
    /// 删除用户DTO
    /// </summary>
    public class DeleteUserDto
    {
        /// <summary>
        /// 用户IDs
        /// </summary>
        public Guid[] UserIds { set; get; }

        /// <summary>
        /// 当前用户Id
        /// </summary>
        public Guid CurrentUserIds { set; get; }
    }
    /// <summary>
    /// 修改密码 DTO
    /// </summary>
    public class ChangePasswordDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// NewPassword
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

    }


    public class UserLoginByPhoneDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemTypeEnum SystemType { get; set; }
    }


    public class GetUserByPhoneDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemTypeEnum SystemType { get; set; }
    }

    /// <summary>
    /// 注册B2C账号 Dto
    /// </summary>
    public class RegisterB2CAccountDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///  密码
        /// </summary>
        public string Password { get; set; }


    }


    public class ResetB2CPasswordDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }


    /// <summary>
    /// 修改密码 DTO
    /// </summary>
    public class ChangePwdDto
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// NewPassword
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// NewPassword
        /// </summary>
        public string OldPassword { get; set; }

    }
}
