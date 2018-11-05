using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Account.Controllers
{
    public class UsersController : BaseController
    {
        /// <summary>
        /// 会员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 会员查看
        /// </summary>
        /// <returns></returns>
        public ActionResult Info(Guid? userId)
        {
            var data4 = WebApiHelper.Get<HttpResponseMsg>(
               "/api/AccountBasic/GetUser", "", "userId=" + userId.Value.ToString(),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.UserInfo = data4.Data.ToString().ToObject<UserDto>();
            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> Search(UserSearchDto param)
        {
            param.SystemType = SystemTypeEnum.TrainingInstitutions;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/AccountBasic/GetMemberList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult ActiveUser(Guid[] userIds)
        {
            ChangeUserStatusDto dto = new ChangeUserStatusDto();
            dto.UserIds = userIds;
            dto.UserStatus = EnrolmentPlatform.Project.DTO.Enums.StatusBaseEnum.Enabled;
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/AccountBasic/ChangeUserStatus",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data.IsSuccess == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = data.Info });
            }
        }


        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult InActiveUser(Guid[] userIds)
        {
            ChangeUserStatusDto dto = new ChangeUserStatusDto();
            dto.UserIds = userIds;
            dto.UserStatus = EnrolmentPlatform.Project.DTO.Enums.StatusBaseEnum.Disabled;
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/AccountBasic/ChangeUserStatus",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data.IsSuccess == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = data.Info });
            }
        }
    }
}