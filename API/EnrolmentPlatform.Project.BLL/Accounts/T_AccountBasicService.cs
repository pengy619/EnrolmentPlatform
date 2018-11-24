using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class T_AccountBasicService : BaseService<T_AccountBasic>, IT_AccountBasicService, IInterceptorLogic
    {
        private IT_AccountBasicRepository AccountRepository;

        public T_AccountBasicService(IT_AccountBasicRepository accountRepository)
        {
            this.AccountRepository = accountRepository;
        }

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = AccountRepository;
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int AddUser(UserDto dto)
        {
            dto.UserId = Guid.NewGuid();
            dto.Password = Md5.Md5Hash(dto.Password + dto.UserId.ToString());
            return this.AccountRepository.AddUser(dto);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int UpdateUser(UserDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                dto.Password = Md5.Md5Hash(dto.Password + dto.UserId.ToString());
            }
            return this.AccountRepository.UpdateUser(dto);
        }



        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public UserDto GetUser(Guid userId)
        {
            return this.AccountRepository.GetUser(userId);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="roleIds">用户ID集合</param>
        /// <returns></returns>
        public bool DeleteUser(DeleteUserDto userDto)
        {
            return this.AccountRepository.DeleteUser(userDto);
        }

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        public List<UserDto> GetUserList(UserSearchDto param, out int reCount)
        {
            return this.AccountRepository.GetUserList(param, out reCount);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public UserLoginDto UserLogin(UserLoginRequestDto dto, out string msg)
        {
            return this.AccountRepository.UserLogin(dto, out msg);
        }

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>

        public List<AdminUserListDto> GetAdminUserList(UserSearchDto param, out int reCount)
        {
            return this.AccountRepository.GetAdminUserList(param, out reCount);
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userDto">dto</param>
        /// <returns></returns>
        public bool ChangeUserStatus(ChangeUserStatusDto userDto)
        {
            return this.AccountRepository.ChangeUserStatus(userDto);
        }

        /// <summary>
        /// 获得C端用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>

        public List<UserDto> GetMemberList(UserSearchDto param, out int reCount)
        {
            return this.AccountRepository.GetMemberList(param, out reCount);
        }

        /// <summary>
        ///  手机号登录 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public UserLoginDto UserLoginByPhone(string phone, SystemTypeEnum systemType)
        {
            var userInfo = AccountRepository.LoadEntities(o => o.Phone == phone && o.Classify == (int)systemType).FirstOrDefault();
            if (userInfo == null)
            {
                //新增用户数据，TODO
                userInfo = new T_AccountBasic()
                {
                    EnterpriseId = Guid.Parse("1EF41439-9E4B-4ACD-BA5F-F7071C5DEA88"),
                    Classify = (int)systemType,
                    AccountNo = phone,
                    DepartmentId = Guid.Empty,
                    Id = Guid.NewGuid(),
                    IsMaster = true,
                    Phone = phone,
                    RoleId = Guid.Empty,
                    Status = 2,
                    CreatorUserId = Guid.NewGuid(),
                    CreatorTime = DateTime.Now
                };
                CurrentRepository.AddEntity(userInfo);
            }

            return new UserLoginDto()
            {
                RoleId = userInfo.RoleId,
                EnterpriseId = userInfo.EnterpriseId,
                IsMaster = userInfo.IsMaster,
                Phone = userInfo.Phone,
                UserAccount = userInfo.AccountNo,
                UserId = userInfo.Id,
                UserName = userInfo.RealName
            };
        }

        /// <summary>
        /// 修改密码  
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg ChangePassword(ChangePasswordDto dto)
        {
            ResultMsg result = new ResultMsg() { IsSuccess = false };
            var user = AccountRepository.FindEntityById(dto.Id);
            if (user == null)
            {
                result.Info = "参数有误！";
                return result;
            }
            user.PassWord = Md5.Md5Hash(dto.NewPassword + dto.Id);
            user.LastModifyTime = DateTime.Now;
            user.LastModifyUserId = user.Id;
            AccountRepository.UpdateEntity(user);
            result.IsSuccess = true;
            return result;
        }

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public UserDto GetUserByPhone(GetUserByPhoneDto dto)
        {
            var userInfo = AccountRepository.LoadEntities(o => o.Phone == dto.Phone && o.Classify == (int)dto.SystemType).FirstOrDefault();
            if (userInfo != null)
            {
                return new UserDto()
                {
                    UserId = userInfo.Id,
                    UserName = userInfo.RealName,
                    RoleId = userInfo.RoleId,
                    UserAccount = userInfo.AccountNo,
                    IntStatus = userInfo.Status,
                    CreateTime = userInfo.CreatorTime,
                    SystemType = (SystemTypeEnum)userInfo.Classify,
                    EnterpriseId = userInfo.EnterpriseId,
                    Phone = userInfo.Phone,
                    Comment = userInfo.Remark,
                    IsMaster = userInfo.IsMaster,
                    Sex = userInfo.Sex
                };
            }
            return null;
        }

        /// <summary>
        /// 修改密码  
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg ChangePwd(ChangePwdDto dto)
        {
            ResultMsg result = new ResultMsg() { IsSuccess = false };
            var user = AccountRepository.FindEntityById(dto.UserId);
            if (user == null)
            {
                result.Info = "参数有误！";
                return result;
            }
            dto.OldPassword = Md5.Md5Hash(dto.OldPassword + dto.UserId);
            if (!user.PassWord.Equals(dto.OldPassword))
            {
                result.Info = "旧密码有误！";
                return result;
            }
            user.PassWord = Md5.Md5Hash(dto.NewPassword + dto.UserId);
            user.LastModifyTime = DateTime.Now;
            user.LastModifyUserId = user.Id;
            AccountRepository.UpdateEntity(user);
            result.IsSuccess = true;
            return result;
        }


        #region 供应商子账户

        /// <summary>
        /// 新增供应商用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int AddSupplierUser(SupplierUserDto dto)
        {
            return this.AccountRepository.AddSupplierUser(dto);
        }

        /// <summary>
        /// 修改供应商用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        public int UpdateSupplierUser(SupplierUserDto dto)
        {
            return this.AccountRepository.UpdateSupplierUser(dto);
        }

        /// <summary>
        /// 获得供应商子账户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public SupplierUserDto GetSupplierUser(Guid userId)
        {
            return this.AccountRepository.GetSupplierUser(userId);
        }
        #endregion
    }
}
