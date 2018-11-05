using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Filter;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Controllers
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
        /// 账号ID（目前用户ID）
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
        /// 供应商ID
        /// </summary>
        public Guid SupplierId
        {
            get
            {
                return this.LoginInfo.EnterpriseId;
            }
        }

        /// <summary>
        /// 用户手机
        /// </summary>
        public string Phone
        {
            get
            {
                return this.LoginInfo.Phone;
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