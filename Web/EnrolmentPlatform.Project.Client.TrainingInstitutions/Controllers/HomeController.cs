using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.Infrastructure;
using System.Xml;
using EnrolmentPlatform.Project.DTO.Systems;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers
{
    public class HomeController : BaseController
    {
  
        
        /// <summary>
        /// 没有权限
        /// </summary>
        /// <returns></returns>
        public ActionResult NoPermission()
        {
            return View();
        }


        

        public async Task<string> LogSettingForTable(LogSettingDTO param)
        {
            param.IsFilterAccount = true;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
             "/api/LogSetting/FindLogSettingByKeyForGridData",
             JsonConvert.SerializeObject(param),
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }
        public async Task<string> Search(LogSettingDTO param)
        {
            param.IsFilterAccount = true;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/LogSetting/FindLogSettingByKeyForGridData",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        [ValidateInput(false)]
        [HttpPost]
        public async Task<JsonResult> GetDataInfo(string startTime, string endTime)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("startTime", startTime);
            param.Add("endTime", endTime);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var data = await WebApiHelper.GetAsync<HttpResponseMsg>(
            "/api/SystemMessage/GetHomeInfoForAdminDtoByTime", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            HomeInfoForAdminDto homeInfoForAdminDto = data.Data.ToString().ToObject<HomeInfoForAdminDto>();
            return Json(homeInfoForAdminDto, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用于管理员添加权限
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionPermissions()
        {
            return View();
        }

        /// <summary>
        /// 安全退去
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            EnrolmentPlatform.Project.Client.TrainingInstitutions.Filter.LoginInfoHandle.ClearCookie();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("roleId", base.RoleId.ToString());
            parames.Add("systemClassify", ((int)SystemTypeEnum.TrainingInstitutions).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);

            WebApiHelper.Get<HttpResponseMsg>(
                 "/api/Role/ClearRoleAndSystemPremission", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());

            return RedirectToAction("Index", "Login", new { area = "" });
        }

        [ChildActionOnly]
        public ActionResult NavigationMenu(string areaName, string controllerName, string action)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("area", areaName);
            param.Add("controller", controllerName);
            param.Add("action", action);
            param.Add("roleId", base.RoleId.ToString());
            param.Add("SystemTypeEnum", ((int)SystemTypeEnum.TrainingInstitutions).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);

            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetPermissionsByLocation", parameters.Item1, parameters.Item2,
           ConfigurationManager.AppSettings["StaffId"].ToInt());

            List<RolePermissionDto> rolePermissionDtoLst = data.Data.ToString().ToObject<List<RolePermissionDto>>();
            return PartialView("_navigationMenu", rolePermissionDtoLst);
        }


       
    }
}