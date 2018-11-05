using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Finance;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Finance.Controllers
{
    public class WithdrawController : BaseController
    {
        // GET: Finance/Withdraw
        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///  获取  提现申请列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetApplyCashList(ApplyCashListDto request)
        {
            request.EnterpriseId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/ApplyCash/GetApplyCashList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }


        /// <summary>
        /// 提现申请
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Option()
        {
            var bankList = WebApiHelper.Get<HttpResponseMsg>("/api/BankCard/GetListByEnterpriseId", "", "id=" + this.SupplierId.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            var SupplierBalance = WebApiHelper.Get<HttpResponseMsg>("/api/ApplyCash/GetSupplierBalance", "", "id=" + this.SupplierId.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.SupplierBalance = SupplierBalance.Data.ToString().ToObject<SupplierBalanceDto>();
            ViewBag.BankList = bankList.Data.ToString().ToList<BankCardListDto>();
            return View(new AddApplyCashDto());
        }

        /// <summary>
        /// 提现申请提交 
        /// </summary>
        /// <param name="addApplyCashDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Option(AddApplyCashDto addApplyCashDto)
        {
            addApplyCashDto.CreatorUserId = this.UserId;
            addApplyCashDto.CreatorAccount = this.UserAccount;
            addApplyCashDto.EnterpriseId = this.SupplierId;
            var res = WebApiHelper.Post<HttpResponseMsg>("/api/ApplyCash/AddApplyCash", JsonConvert.SerializeObject(addApplyCashDto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res);
        }


        /// <summary>
        /// 提现申请详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid id)
        {
            var res = WebApiHelper.Get<HttpResponseMsg>("/api/ApplyCash/GetApplyCashDetail", "", "id=" + id.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            ApplyCashDetailDto applyCashDetailDto = new ApplyCashDetailDto();
            if (res.IsSuccess)
                applyCashDetailDto = res.Data.ToString().ToObject<ApplyCashDetailDto>();
            return View(applyCashDetailDto);
        }

        /// <summary>
        /// 提现密码设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Password()
        {
            var ret = WebApiHelper.Get<HttpResponseMsg>(
               "/api/Enterprise/GetSupplierInfo", ""
               , "supplierId=" + this.SupplierId.ToString(),
              ConfigurationManager.AppSettings["StaffId"].ToInt());
            SupplierEnterpriseGetDto dto = ret.Data.ToString().ToObject<SupplierEnterpriseGetDto>();
            ViewBag.Phone = dto.Phone;
            return View();
        }


        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool SendVeriyCode(string phone)
        {
            //如果验证码存在
            string codeStr = CookieHelper.GetCookieValue("CashPassWordVeriyCode");
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
            dto.Phone = phone;
            //发送短信验证码
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/SMS/SendVerificationCode", JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());

            //验证码放入Cookie
            string code = ret.Data.ToString();
            codeStr = dto.Phone + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + code;
            CookieHelper.SetCookieValue("CashPassWordVeriyCode", DESEncrypt.Encrypt(codeStr), 10);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Password(string password, string code)
        {
            HttpResponseMsg result = new HttpResponseMsg() { IsSuccess = false };
            //如果验证码存在
            string codeStr = CookieHelper.GetCookieValue("CashPassWordVeriyCode");
            if (string.IsNullOrEmpty(codeStr))
            {
                //验证码错误
                result.Info = "验证码错误";
                return Json(result);
            }
            string[] arr = DESEncrypt.Decrypt(codeStr).Split('|');
            if (arr.Length != 3 || arr[2] != code)
            {
                //验证码错误
                result.Info = "验证码错误";
                return Json(result);
            }
            SetCashPasswordDto setCashPasswordDto = new SetCashPasswordDto() { EnterpriseId = this.SupplierId, OperatorId = this.UserId, Password = password };
            result = WebApiHelper.Post<HttpResponseMsg>("/api/ApplyCash/SetCashPassword", JsonConvert.SerializeObject(setCashPasswordDto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(result);
        }

    }
}