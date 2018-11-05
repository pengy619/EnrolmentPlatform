using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Filter;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
namespace EnrolmentPlatform.Project.Client.Admin.Controllers
{
    [AuthorityAttribute]
    
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        private UserLoginDto _loginInfo = null;
        private UserLoginDto LoginInfo
        {
            get
            {
                if (this._loginInfo == null)
                {
                    this._loginInfo = LoginInfoHandle.GetLoginInfo();
                }
                return this._loginInfo;
            }
        }

        /// <summary>
        /// 企业ID 企业类型为景区
        /// </summary>
        public Guid EnterpriseId
        {
            get
            {
                return this.LoginInfo.EnterpriseId;
            }
        }

        /// <summary>
        /// 账号ID
        /// </summary>
        public Guid UserId
        {
            get
            {
                return this.LoginInfo.UserId;
            }
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId
        {
            get
            {
                return this.LoginInfo.RoleId;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserUser
        {
            get
            {
                return this.LoginInfo.UserName;
            }
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount
        {
            get
            {
                return this.LoginInfo.UserAccount;
            }
        }

        /// <summary>
        /// 是否是主账号
        /// </summary>
        public bool IsMaster
        {
            get
            {
                return this.LoginInfo.IsMaster;
            }
        }
    }
}