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
using EnrolmentPlatform.Project.Client.Admin.Filter;

namespace EnrolmentPlatform.Project.Client.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登陆处理
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoginOp(string account, string pwd, string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                if (DESEncrypt.Encrypt(code.ToLower()) != CookieHelper.GetCookieValue("CurrentValidateCode"))
                {
                    return Json(new { ret = false, msg = "验证码错误" });
                }
            }
            //登陆请求DTO
            UserLoginRequestDto loginReq = new UserLoginRequestDto()
            {
                Password = pwd,
                SystemType = EnrolmentPlatform.Project.DTO.Enums.Systems.SystemTypeEnum.ChannelCenter,
                UserAccount = account
            };

            //登陆请求
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/AccountBasic/UserLogin", JsonConvert.SerializeObject(loginReq),
               ConfigurationManager.AppSettings["StaffId"].ToInt());

            //登陆失败
            if (ret.IsSuccess == false)
            {
                return Json(new { ret = false, msg = ret.Info });
            }
            else
            {
                UserLoginDto userDto = ret.Data.ToString().ToObject<UserLoginDto>();
                //保存登陆状态
                LoginInfoHandle.SetLoginInfo(userDto);
                LoginLogDto loginLogDtp = new LoginLogDto
                {
                    IP = Net.Ip,
                    Account = account,
                    AccountId = userDto.UserId
                };
                //登录日志
                WebApiHelper.Post<HttpResponseMsg>(
                     "/api/LoginLog/AddLoginLog", JsonConvert.SerializeObject(loginLogDtp),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                //返回结果
                return Json(new { ret = true });
            }
        }
    }
}