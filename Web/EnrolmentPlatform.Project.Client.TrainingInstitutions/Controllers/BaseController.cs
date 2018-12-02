using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Filter;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Basics;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers
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
        /// 企业类型 为游客服务中心
        /// </summary>
        public Guid EnterpriseId
        {
            get
            {
                return this.LoginInfo.EnterpriseId;
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
        public string UserName
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

        #region Service

        /// <summary>
        /// 订单接口
        /// </summary>
        public static IT_OrderService OrderService = DIContainer.Resolve<IT_OrderService>();

        /// <summary>
        /// 基础数据接口
        /// </summary>
        public static IT_MetadataService MetadataService= DIContainer.Resolve<IT_MetadataService>();

        /// <summary>
        /// 层级接口
        /// </summary>
        public static IT_SchoolLevelMajorService LevelService= DIContainer.Resolve<IT_SchoolLevelMajorService>();

        #endregion
    }
}