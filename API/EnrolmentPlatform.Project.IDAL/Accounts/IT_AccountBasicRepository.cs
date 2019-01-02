using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IDAL.Accounts
{
    /// <summary>
    /// 账号数据处理接口
    /// </summary>
    public interface IT_AccountBasicRepository : IBaseRepository<T_AccountBasic>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="accountId">账号ID</param>
        /// <param name="realOldPwd">加密后原始密码</param>
        /// <param name="realNewPwd">加密后新密码</param>
        /// <returns></returns>
        bool ChangePwd(Guid accountId, string realOldPwd, string realNewPwd);

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
    }
}
