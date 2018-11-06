using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using EnrolmentPlatform.Project.Client.LearningCenter.Filter;
using EnrolmentPlatform.Project.DTO.Enums.Systems;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemInfo()
        {
            //状态
            ViewBag.MessageStatus = EnumDescriptionHelper.GetItemValueList<MessageStatusEnum, int>().ToList();
            return View();
        }

        /// <summary>
        /// 没有权限
        /// </summary>
        /// <returns></returns>
        public ActionResult NoPermission()
        {
            return View();
        }

        /// <summary>
        /// 系统信息列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> SystemMessageList(ParamForSystemMessageDto param)
        {
            param.EnterpriseId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SystemMessage/GetSystemMessageForSupplierForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 系统信息已读
        /// </summary>
        /// <param name="messageIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> MessageOnRead(List<Guid> messageIds)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SystemMessage/MessageOnReadForSupplier",
                JsonConvert.SerializeObject(messageIds),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }
        public async Task<string> LogSettingForTable(LogSettingDTO param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
             "/api/LogSetting/FindLogSettingByKeyForGridData",
             JsonConvert.SerializeObject(param),
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        [ChildActionOnly]
        public ActionResult NavigationMenu(string areaName, string controllerName, string action)
        {

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("area", areaName);
            param.Add("controller", controllerName);
            param.Add("action", action);
            param.Add("roleId", base.RoleId.ToString());
            param.Add("SystemTypeEnum", ((int)SystemTypeEnum.LearningCenter).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);

            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetPermissionsByLocation", parameters.Item1, parameters.Item2,
           ConfigurationManager.AppSettings["StaffId"].ToInt());

            List<RolePermissionDto> rolePermissionDtoLst = data.Data.ToString().ToObject<List<RolePermissionDto>>();
            return PartialView("_navigationMenu", rolePermissionDtoLst);
        }


        /// <summary>
        /// 安全退去
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            LoginInfoHandle.ClearCookie();

            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("roleId", base.RoleId.ToString());
            parames.Add("systemClassify", ((int)SystemTypeEnum.LearningCenter).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);

            WebApiHelper.Get<HttpResponseMsg>(
                 "/api/Role/ClearRoleAndSystemPremission", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());

            return RedirectToAction("Index", "Login", new { area = "" });
        }


        [ValidateInput(false)]
        [HttpPost]
        public async Task<JsonResult> GetDataInfo(string startTime, string endTime)
        {
            if (startTime == endTime)
            {
                endTime = DateTime.Parse(endTime).AddDays(1).ToString("yyyy-MM-dd");
            }
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("startTime", startTime);
            param.Add("endTime", endTime);
            param.Add("supplierId", base.SupplierId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var data = await WebApiHelper.GetAsync<HttpResponseMsg>(
            "/api/SystemMessage/GetHomeInfoForSupplierByTime", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            HomeInfoForAdminDto homeInfoForAdminDto = data.Data.ToString().ToObject<HomeInfoForAdminDto>();
            return Json(homeInfoForAdminDto, JsonRequestBehavior.AllowGet);
        }
    }
}