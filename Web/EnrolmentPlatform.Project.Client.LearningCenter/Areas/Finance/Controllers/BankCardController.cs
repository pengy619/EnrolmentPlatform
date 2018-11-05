using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Finance;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Finance.Controllers
{
    public class BankCardController : BaseController
    {
        // GET: Finance/BankCard
        /// <summary>
        /// 银行卡管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<BankCardListDto> list = new List<BankCardListDto>();
            var ret = WebApiHelper.Get<HttpResponseMsg>(
           "/api/BankCard/GetListByEnterpriseId", ""
           , "id=" + this.SupplierId.ToString(),
          ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<BankCardListDto>();
            }
            return View(list);
        }


        /// <summary>
        /// 银行卡新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Option()
        {
            BankCardDto model = new BankCardDto();
            return View(model);
        }
        /// <summary>
        /// 银行卡新增 提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Option(BankCardDto model)
        {
            HttpResponseMsg result = new HttpResponseMsg() { IsSuccess = false };
            //如果验证码存在
            string codeStr = CookieHelper.GetCookieValue("AddBankCardVeriyCode");
            if (string.IsNullOrEmpty(codeStr))
            {
                //验证码错误
                result.Info = "验证码错误";
                return Json(result);
            }
            string[] arr = DESEncrypt.Decrypt(codeStr).Split('|');
            if (arr.Length != 3 || arr[2] != Request["VerificationCode"])
            {
                //验证码错误
                result.Info = "验证码错误";
                return Json(result);
            }

            model.CreatorUserId = this.UserId;
            model.EnterpriseId = this.SupplierId;
            model.CreatorAccount = this.UserAccount;
            var ret = WebApiHelper.Post<HttpResponseMsg>("/api/BankCard/AddOrEditBankCard", JsonConvert.SerializeObject(model), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendVeriyCode(string phone)
        {

            //如果验证码存在
            string codeStr = CookieHelper.GetCookieValue("AddBankCardVeriyCode");
            if (!string.IsNullOrEmpty(codeStr))
            {
                string[] arr = DESEncrypt.Decrypt(codeStr).Split('|');
                if (arr.Length == 3)
                {
                    //如果当前时间在 上一次发送的时间 + 59秒内
                    if (DateTime.Parse(arr[1]).AddSeconds(59) > DateTime.Now)
                    {
                        //不允许发送
                        return Json(new { IsSuccess = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            SendVeriyCodeDto dto = new SendVeriyCodeDto();
            dto.Phone = phone;
            //发送短信验证码
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/SMS/SendVerificationCode", JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());

            //验证码放入Cookie
            string code = ret.Data.ToString();
            codeStr = dto.Phone + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + code;
            CookieHelper.SetCookieValue("AddBankCardVeriyCode", DESEncrypt.Encrypt(codeStr), 10);
            return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        ///  解绑银行卡
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UnbindingBankCard(UnbindingBankCardDto dto)
        {
            dto.LastModifyUserId = this.UserId;
            dto.Operator = this.UserAccount;
            var ret = WebApiHelper.Post<HttpResponseMsg>("/api/BankCard/UnbindingBankCard", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

    }
}