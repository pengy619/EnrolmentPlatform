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

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Setting.Controllers
{
    public class StaffController : BaseController
    {
        // GET: Setting/Staff
        /// <summary>
        /// 员工管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 员工新增修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? userId)
        {
            //当前操作状态
            ViewBag.OPStatus = 1;

            //获得所有可用角色列表
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("type", ((int)SystemTypeEnum.ChannelCenter).ToString());
            parames.Add("enterpriseId", this.EnterpriseId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetActiveRoleList", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.RoleList = data.Data.ToString().ToObject<List<RoleDto>>();

            //获得所有部门列表
            var data2 = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Department/GetDepartmentList", "", "",
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.DepartmentList = data2.Data.ToString().ToObject<List<DepartmentDto>>();

            //获得所有岗位列表
            var data3 = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Position/GetJobList", "", "",
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.JobList = data3.Data.ToString().ToObject<List<JobDto>>();

            //如果是修改
            if (userId.HasValue)
            {
                var data4 = WebApiHelper.Get<HttpResponseMsg>(
                "/api/AccountBasic/GetUser", "", "userId=" + userId.Value.ToString(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
                ViewBag.OPStatus = 2;
                ViewBag.UserInfo = data4.Data.ToString().ToObject<UserDto>();
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
            param.EnterpriseId = this.EnterpriseId;
            param.SystemType = SystemTypeEnum.ChannelCenter;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/AccountBasic/GetAdminUserList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult SaveUser(UserDto dto)
        {
            dto.EnterpriseId = this.EnterpriseId;
            dto.SystemType = SystemTypeEnum.ChannelCenter;
            dto.CreateUserId = this.UserId;
            dto.CreateAccount = this.UserAccount;
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/AccountBasic/SaveUser",
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