using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Setting.Controllers
{
    public class LogController : BaseController
    {
        // GET: Setting/Log
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <returns></returns>
        public ActionResult Option()
        {
            return View();
        }
        public async Task<string> SearchLoginLog(LoginLogDto param)
        {
            param.EnterpriseId = base.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/LoginLog/GetEnterpriseLoginLog",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }
        public async Task<string> SearchOptionLog(LogSettingDTO param)
        {
            param.EnterpriseId = base.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/LogSetting/FindLogSettingBEnterpriseIdForGridData",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }
    }
}