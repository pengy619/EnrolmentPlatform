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

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Operate.Controllers
{
    public class SettingBasicController : BaseController
    {
        /// <summary>
        /// 用户注册协议
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserProtocol()
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/SystemBasicSetting/GetUserProtocolSet", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                return View(msg.Data);
            }
            else
            {
                throw new Exception(msg.Info);
            }
        }

        /// <summary>
        /// 保存用户注册协议
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UserProtocol(UserProtocolSetDTO dto)
        {
            dto.UpdateUserName = base.UserAccount;
            dto.UpdateUserId = base.UserId;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/SystemBasicSetting/UserProtocolSet",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }

        /// <summary>
        /// Logo设置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Logo()
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/SystemBasicSetting/GetTotalStationSet", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                return View(msg.Data.ToString().ToObject<TotalStationSetDTO>());
            }
            else
            {
                throw new Exception(msg.Info);
            }
        }
    }
}