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
            //供应商添加测试
            EnterpriseAddDto entity = new EnterpriseAddDto();
            //entity.Address = "1111";
            //entity.AddressId = Guid.Parse("FDD67AE3-E2D6-4C79-A033-D07CA5044A7C");
            //entity.BusinessLicenseCode = "1111";
            //entity.BusinessLicenseUrl = "http://e.hiphotos.baidu.com/image/pic/item/3801213fb80e7bec00e429f3232eb9389b506be1.jpg";
            //entity.IDCardReverseUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1521450856332&di=ba8fe96aa06d4e54ab47052814b2ee2a&imgtype=0&src=http%3A%2F%2Fimgsrc.baidu.com%2Fimage%2Fc0%253Dpixel_huitu%252C0%252C0%252C294%252C40%2Fsign%3Db05d0b3c38fa828bc52e95a394672458%2Fd788d43f8794a4c2717d681205f41bd5ad6e39a8.jpg";
            //entity.IDCardUpwardsUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1521450872207&di=46595d6473fd6caaf2657d7703a1d0af&imgtype=0&src=http%3A%2F%2F5b0988e595225.cdn.sohucs.com%2Fimages%2F20171111%2F8a2058c30d614674a4fa7e40cf171ebf.jpeg";
            //entity.BusinessRang = new int[] {1 };
            //entity.BusinessType = EnrolmentPlatform.Project.DTO.Enums.Enterprise.EnterpriseBusinessTypeEnum.CommercialTenant;
            //entity.Classify = EnrolmentPlatform.Project.DTO.Enums.Enterprise.EnterpriceTypeEnum.Supplier;
            //entity.Contact = "联系人";
            //entity.DepositAmount = 10;
            //entity.UserAccount = "admin";
            //entity.EnterpriseName = "测试";
            //entity.ExpireDate = DateTime.Now.AddYears(1).ToShortDateString();
            //entity.Phone = "110";
            //entity.Rate = 0.001M;
            //entity.SettlementCycle = EnrolmentPlatform.Project.DTO.Enums.Enterprise.SettlementCycleEnum.Week;
            //entity.Status = EnrolmentPlatform.Project.DTO.Enums.Enterprise.StatusEnum.Enable;
            //entity.UserAccount = "supplier001";
            //entity.UserPwd = "123456";
            //entity.CurUserAccount = "测试";
            //entity.CurUserId = Guid.Parse("777e431d-c46b-4f42-b276-4f94289066ab");
            //var data = WebApiHelper.Post<HttpResponseMsg>(
            //    "/api/Enterprise/AddEnterprise",
            //    JsonConvert.SerializeObject(entity),
            //   ConfigurationManager.AppSettings["StaffId"].ToInt());

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
                CookieHelper.ClearCookie("PwdUpdateVeriyCode");
                return 1;
            }
            return 3;
        }
    }
}