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

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Finance.Controllers
{
    public class BasicSettingController : BaseController
    {
        // GET: Finance/BasicSetting
        public async Task<ActionResult> Index()
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/SystemBasicSetting/GetWithdrawRange", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                return View(msg.Data.ToString().ToObject<List<string>>());
            }
            else
            {
                throw new Exception(msg.Info);
            }
        }
        public async Task<ActionResult> SetWithdrawRange(WithdrawRangeDTO dto)
        {
            dto.UpdateUserName = base.UserAccount;
            dto.UpdateUserId = base.UserId;
            var msg =await  WebApiHelper.PostAsync<HttpResponseMsg>("/api/SystemBasicSetting/SetWithdrawRange", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
    }
}