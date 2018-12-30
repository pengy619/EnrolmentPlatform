using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Account.Controllers
{
    public class LearningCenterController : BaseController
    {
        /// <summary>
        /// 学院中心管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 学院中心操作
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? supplierId)
        {
            EnterpriseAddDto dto = new EnterpriseAddDto();
            if (supplierId.HasValue)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("enterpriseId", supplierId.Value.ToString());
                Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
                var ret = WebApiHelper.Get<HttpResponseMsg>(
                    "/api/Enterprise/GetEnterpriseById", parameters.Item1, parameters.Item2,
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                if (ret.IsSuccess)
                {
                    dto = ret.Data.ToString().ToObject<EnterpriseAddDto>();
                }
            }
            return View(dto);
        }

        /// <summary>
        /// 保存学院中心
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SaveSupplier(EnterpriseAddDto dto)
        {
            dto.Classify = SystemTypeEnum.LearningCenter;
            dto.CurUserId = this.UserId;
            dto.CurUserAccount = this.UserAccount;
            if (dto.EnterpriseId.IsEmpty())
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Enterprise/AddEnterprise",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
            else
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Enterprise/UpdateEnterpriseInfo",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
        }
    }
}