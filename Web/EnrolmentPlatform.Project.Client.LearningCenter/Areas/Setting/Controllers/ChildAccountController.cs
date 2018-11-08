using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Setting.Controllers
{
    public class ChildAccountController : BaseController
    {
        // GET: Setting/ChildAccount
        /// <summary>
        /// 子账号列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 子账号新增修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? userId)
        {
            //参数拼装
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("type", ((int)SystemTypeEnum.LearningCenter).ToString());
            parames.Add("enterpriseId", this.SupplierId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            //获得所有可用角色列表
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetActiveRoleList", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.OPStatus = 1;
            ViewBag.RoleList = data.Data.ToString().ToObject<List<RoleDto>>();

            //获得供应商资源列表
            Dictionary<string, string> parames3 = new Dictionary<string, string>();
            parames3.Add("supplierId", this.SupplierId.ToString());
            Tuple<string, string> parameters3 = WebApiHelper.GetQueryString(parames3);
            var data3 = WebApiHelper.Get<HttpResponseMsg>(
            "/api/AccountBasic/GetSupplierVerificationList", parameters3.Item1, parameters3.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());

            //如果是修改
            if (userId.HasValue)
            {
                var data2 = WebApiHelper.Get<HttpResponseMsg>(
                "/api/AccountBasic/GetSupplierUser", "", "userId=" + userId.Value.ToString(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
                ViewBag.OPStatus = 2;
                ViewBag.UserInfo = data2.Data.ToString().ToObject<SupplierUserDto>();
            }
            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> Search(UserSearchDto param)
        {
            param.IsMaster = false;
            param.EnterpriseId = this.SupplierId;
            param.SystemType = EnrolmentPlatform.Project.DTO.Enums.Systems.SystemTypeEnum.LearningCenter;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/AccountBasic/GetUserList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult SaveUser(SupplierUserDto dto)
        {
            dto.EnterpriseId = this.SupplierId;
            dto.SystemType = EnrolmentPlatform.Project.DTO.Enums.Systems.SystemTypeEnum.LearningCenter;
            dto.CreateUserId = this.UserId;
            dto.CreateAccount = this.UserAccount;
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/AccountBasic/SaveSupplierUser",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());

            var ret = 3;
            if (data.IsSuccess == true)
            {
                //修改成功
                ret = 1;
                if (dto.UserId == this.UserId && !string.IsNullOrWhiteSpace(dto.Password))
                {
                    //修改成功，且修改的为当前用户且修改了密码
                    ret = 2;
                }
            }
            return Json(new { ret = ret, msg = data.Info });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult DeleteUser(Guid[] userIds)
        {
            DeleteUserDto userDto = new DeleteUserDto
            {
                CurrentUserIds = base.UserId,
                UserIds = userIds
            };
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/AccountBasic/DeleteUser",
                JsonConvert.SerializeObject(userDto),
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