using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Setting.Controllers
{
    public class AccountManageController : BaseController
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyForPwd()
        {
            ViewBag.Phone = this.Phone;
            return View();
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool SendVeriyCode()
        {
            //如果验证码存在
            string codeStr = CookieHelper.GetCookieValue("TouristCenterPwdUpdateVeriyCode");
            if (!string.IsNullOrEmpty(codeStr))
            {
                string[] arr = DESEncrypt.Decrypt(codeStr).Split('|');
                if (arr.Length == 3)
                {
                    //如果当前时间在 上一次发送的时间 + 59秒内
                    if (DateTime.Parse(arr[1]).AddSeconds(59) > DateTime.Now)
                    {
                        //不允许发送
                        return false;
                    }
                }
            }

            SendVeriyCodeDto dto = new SendVeriyCodeDto();
            dto.Phone = this.Phone;
            //发送短信验证码
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/SMS/SendVerificationCode", JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());

            //验证码放入Cookie
            string code = ret.Data.ToString();
            codeStr = dto.Phone + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + code;
            CookieHelper.SetCookieValue("TouristCenterPwdUpdateVeriyCode", DESEncrypt.Encrypt(codeStr), 10);

            return true;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>1：成功，2：验证码错误，3：原密码错误</returns>
        [HttpPost]
        public int ModifyForPwdOp(string oldPwd, string newPwd, string code)
        {
            //如果验证码存在
            string codeStr = CookieHelper.GetCookieValue("TouristCenterPwdUpdateVeriyCode");
            if (string.IsNullOrEmpty(codeStr))
            {
                //验证码错误
                return 2;
            }

            string[] arr = DESEncrypt.Decrypt(codeStr).Split('|');
            if (arr.Length != 3 || arr[2]!=code)
            {
                //验证码错误
                return 2;
            }

            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("accountId", base.UserId.ToString());
            parames.Add("oldPwd", oldPwd);
            parames.Add("newPwd", newPwd);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            //修改密码
            var ret = WebApiHelper.Get<HttpResponseMsg>("/api/Enterprise/ChangeSupplierPwd",
                parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            
            //修改成功
            if (ret.IsSuccess == true)
            {
                CookieHelper.ClearCookie("TouristCenterPwdUpdateVeriyCode");
                return 1;
            }
            return 3;
        }

        /// <summary>
        /// 子账号列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            //参数拼装
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("type", ((int)SystemTypeEnum.TrainingInstitutions).ToString());
            parames.Add("enterpriseId", this.EnterpriseId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            //获得所有可用角色列表
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetActiveRoleList", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.RoleList = data.Data.ToString().ToObject<List<RoleDto>>();
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
            parames.Add("type", ((int)SystemTypeEnum.TrainingInstitutions).ToString());
            parames.Add("enterpriseId", this.EnterpriseId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            //获得所有可用角色列表
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetActiveRoleList", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.OPStatus = 1;
            ViewBag.RoleList = data.Data.ToString().ToObject<List<RoleDto>>();

            //如果是修改
            if (userId.HasValue)
            {
                var data2 = WebApiHelper.Get<HttpResponseMsg>(
                "/api/AccountBasic/GetUser", "", "userId=" + userId.Value.ToString(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
                ViewBag.OPStatus = 2;
                ViewBag.UserInfo = data2.Data.ToString().ToObject<UserDto>();
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
            param.SystemType = EnrolmentPlatform.Project.DTO.Enums.Systems.SystemTypeEnum.TrainingInstitutions;
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
        public JsonResult SaveUser(UserDto dto)
        {
            dto.EnterpriseId = this.EnterpriseId;
            dto.SystemType = EnrolmentPlatform.Project.DTO.Enums.Systems.SystemTypeEnum.TrainingInstitutions;
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
    }
}