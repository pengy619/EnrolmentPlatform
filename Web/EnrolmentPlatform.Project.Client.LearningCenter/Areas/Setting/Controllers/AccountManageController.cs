using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Setting.Controllers
{
    public class AccountManageController : BaseController
    {
        // GET: Setting/AccountManage

        /// <summary>
        /// 账户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Info()
        {
            //信息获取
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Enterprise/GetSupplierInfo", ""
                , "supplierId=" + this.SupplierId.ToString(),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            SupplierEnterpriseGetDto dto = ret.Data.ToString().ToObject<SupplierEnterpriseGetDto>();
            dto.UserAccount = this.UserAccount;
            ViewBag.EntityData = dto;
            return View();
        }

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
            string codeStr = CookieHelper.GetCookieValue("PwdUpdateVeriyCode");
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
            CookieHelper.SetCookieValue("PwdUpdateVeriyCode", DESEncrypt.Encrypt(codeStr), 10);

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
            string codeStr = CookieHelper.GetCookieValue("PwdUpdateVeriyCode");
            if (string.IsNullOrEmpty(codeStr))
            {
                //验证码错误
                return 2;
            }

            string[] arr = DESEncrypt.Decrypt(codeStr).Split('|');
            if (arr.Length != 3 || arr[2] != code)
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
                CookieHelper.ClearCookie("PwdUpdateVeriyCode");
                return 1;
            }
            return 3;
        }
    }
}