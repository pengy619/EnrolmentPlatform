using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Setting.Controllers
{
    public class ParamForCateringController : BaseController
    {
        /// <summary>
        /// 餐饮参数
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 餐饮参数列表
        /// </summary>
        /// <returns></returns>
        public async Task<string> CateringParamList(CateringParamSearchDto req)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/CateringParam/GetCateringParamPageList",
                JsonConvert.SerializeObject(req),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 添加餐饮参数
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddCateringParam(CateringParamDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/CateringParam/AddCateringParam",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 更新餐饮参数
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> UpdateCateringParam(CateringParamDto dto)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/CateringParam/UpdateCateringParam",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 删除餐饮参数
        /// </summary>
        /// <param name="paramId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteCateringParam(Guid paramId)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/CateringParam/DeleteCateringParam",
                JsonConvert.SerializeObject(paramId),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }
    }
}