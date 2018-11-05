using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Setting.Controllers
{
    public class LogController : BaseController
    {
        // GET: Setting/Log

        #region 登录日志
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        public async Task<string> SearchLoginLog(LoginLogDto param)
        {
            param.EnterpriseId = base.EnterpriseId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/LoginLog/GetEnterpriseLoginLog",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        #endregion

        #region 操作日志
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Option()
        {
            return View();
        }
        public async Task<string> SearchOptionLog(LogSettingDTO param)
        {
            param.EnterpriseId = base.EnterpriseId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/LogSetting/FindLogSettingBEnterpriseIdForGridData",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        #endregion


    }
}