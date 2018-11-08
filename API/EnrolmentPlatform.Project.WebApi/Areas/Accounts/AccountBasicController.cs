using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.WebApi.Areas.Accounts
{
    /// <summary>
    /// 用户API
    /// </summary>
    public class AccountBasicController : ApiController
    {
        protected IT_AccountBasicService UserService;
        private ILifetimeScope _scope;

        public AccountBasicController(ILifetimeScope scope)
        {
            this._scope = scope;
            this.UserService = _scope.Resolve<IT_AccountBasicService>();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SaveUser(UserDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int ret = 0;
                if (dto.UserId.HasValue)
                {
                    ret = this.UserService.UpdateUser(dto);
                }
                else
                {
                    ret = this.UserService.AddUser(dto);
                }

                if (ret == 1)
                {
                    _resultMsg.IsSuccess = true;
                }
                else if (ret == 2)
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "存在重复用户账号。";
                }
                else
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "保存失败。";
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetUser(Guid userId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                UserDto ret = this.UserService.GetUser(userId);
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = ret;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteUser(DeleteUserDto userDto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                bool ret = this.UserService.DeleteUser(userDto);
                _resultMsg.IsSuccess = ret;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userDto">userDto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> ChangeUserStatus(ChangeUserStatusDto userDto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                bool ret = this.UserService.ChangeUserStatus(userDto);
                _resultMsg.IsSuccess = ret;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetUserList(UserSearchDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int reCount = 0;
                var lst = this.UserService.GetUserList(param, out reCount);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = reCount,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto">登陆参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UserLogin(UserLoginRequestDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                string msg = "";
                var loginInfo = this.UserService.UserLogin(dto, out msg);
                if (loginInfo != null)
                {
                    _resultMsg.IsSuccess = true;
                    _resultMsg.Data = loginInfo;
                }
                else
                {
                    _resultMsg.IsSuccess = false;
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        _resultMsg.Info = msg;
                    }
                    else
                    {
                        _resultMsg.Info = "登录失败，账号密码错误。";
                    }
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得景区后台用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetAdminUserList(UserSearchDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int reCount = 0;
                var lst = this.UserService.GetAdminUserList(param, out reCount);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = reCount,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得C端用户列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns></returns>
        [HttpPost]

        public async Task<HttpResponseMessage> GetMemberList(UserSearchDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int reCount = 0;
                var lst = this.UserService.GetMemberList(param, out reCount);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = reCount,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }

        #region B2C会员

        /// <summary>
        /// 获得B2C会员信息
        /// </summary>
        /// <param name="memberId">会员ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> FindB2CMemberInfo(Guid memberId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = this.UserService.FindB2CMemberInfo(memberId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改B2C会员信息
        /// </summary>
        /// <param name="dto">会员信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateB2CMemberInfo(B2CMemberDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = this.UserService.UpdateB2CMemberInfo(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改B2C会员头像
        /// </summary>
        /// <param name="dto">会员信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateB2CMemberInfoPic(B2CMemberDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = this.UserService.UpdateB2CMemberInfoPic(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        #endregion

        /// <summary>
        ///   手机号登录 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="systemType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UserLoginByPhone(UserLoginByPhoneDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg
                {
                    Data = this.UserService.UserLoginByPhone(dto.Phone, dto.SystemType)
                };
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetUserByPhone(GetUserByPhoneDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg
                {
                    Data = this.UserService.GetUserByPhone(dto)
                };
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改密码 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> ChangePassword(ChangePasswordDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this.UserService.ChangePassword(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改密码 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> ChangePwd(ChangePwdDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this.UserService.ChangePwd(dto);
                return _resultMsg.ResponseMessage();
            });
        }

        #region 供应商子账户

        /// <summary>
        /// 保存供应商用户
        /// </summary>
        /// <returns>1：成功，2：存在相同账号，3：失败</returns>
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SaveSupplierUser(SupplierUserDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int ret = 0;
                if (dto.UserId.HasValue)
                {
                    ret = this.UserService.UpdateSupplierUser(dto);
                }
                else
                {
                    ret = this.UserService.AddSupplierUser(dto);
                }

                if (ret == 1)
                {
                    _resultMsg.IsSuccess = true;
                }
                else if (ret == 2)
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "存在重复用户账号。";
                }
                else
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "保存失败。";
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得供应商子账户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetSupplierUser(Guid userId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                SupplierUserDto ret = this.UserService.GetSupplierUser(userId);
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = ret;
                return _resultMsg.ResponseMessage();
            });
        }

        #endregion
    }
}
