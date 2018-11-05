using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.IBLL.Accounts
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IT_AccountBasicService : IBaseService<T_AccountBasic>
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        int AddUser(UserDto dto);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        int UpdateUser(UserDto dto);

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        UserDto GetUser(Guid userId);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userDto">DeleteUserDto</param>
        /// <returns></returns>
        bool DeleteUser(DeleteUserDto userDto);

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        List<UserDto> GetUserList(UserSearchDto param, out int reCount);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        UserLoginDto UserLogin(UserLoginRequestDto dto, out string msg);

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        List<AdminUserListDto> GetAdminUserList(UserSearchDto param, out int reCount);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userDto">dto</param>
        /// <returns></returns>
        bool ChangeUserStatus(ChangeUserStatusDto userDto);

        /// <summary>
        /// 获得C端用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        List<UserDto> GetMemberList(UserSearchDto param, out int reCount);

        /// <summary>
        /// 手机号登录 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="systemType"></param>
        /// <returns></returns>
        UserLoginDto UserLoginByPhone(string phone, SystemTypeEnum systemType);

        #region B2C会员

        /// <summary>
        /// 获得B2C会员信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        B2CMemberDto FindB2CMemberInfo(Guid memberId);

        /// <summary>
        /// 修改B2C会员信息
        /// </summary>
        /// <param name="dto">会员信息</param>
        /// <returns></returns>
        bool UpdateB2CMemberInfo(B2CMemberDto dto);

        /// <summary>
        /// 修改B2C会员头像
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool UpdateB2CMemberInfoPic(B2CMemberDto dto);

        #endregion

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg ChangePassword(ChangePasswordDto dto);

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        UserDto GetUserByPhone(GetUserByPhoneDto dto);

        /// <summary>
        /// 修改密码  
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg ChangePwd(ChangePwdDto dto);


        #region 供应商子账户

        /// <summary>
        /// 新增供应商用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        int AddSupplierUser(SupplierUserDto dto);

        /// <summary>
        /// 修改供应商用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        int UpdateSupplierUser(SupplierUserDto dto);

        /// <summary>
        /// 获得供应商子账户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        SupplierUserDto GetSupplierUser(Guid userId);

        #endregion
    }
}
