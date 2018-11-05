using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Finance;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Finance.Controllers
{
    public class WithdrawController : BaseController
    {
        /// <summary>
        /// 体现审核
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }


        /// <summary>
        /// 获取 提现申请列表  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetApplyCashList(ApplyCashListDto request)
        {

            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/ApplyCash/GetApplyCashList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();

        }


        /// <summary>
        /// 体现详情
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
        /// 银行卡管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BankCard()
        {
            return View();
        }

        /// <summary>
        ///  获取 银行卡分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetPageList(BankCardListRequestDto request)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/BankCard/GetPageList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 审核提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AuditApplyCash(AuditApplyCashDto request)
        {
            request.OperatorId = UserId;
            request.Operator = UserAccount;
            var data = WebApiHelper.Post<HttpResponseMsg>("/api/ApplyCash/AuditApplyCash", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data);
        }

        /// <summary>
        /// 银行卡详情
        /// </summary>
        /// <returns></returns>
        public ActionResult BankDetail(Guid id)
        {
            var res = WebApiHelper.Get<HttpResponseMsg>("/api/BankCard/GetBankCardDetail", "", "id=" + id.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            BankCardDetailDto bankCardDetailDto = new BankCardDetailDto();
            if (res.IsSuccess)
                bankCardDetailDto = res.Data.ToString().ToObject<BankCardDetailDto>();

            return View(bankCardDetailDto);
        }
    }
}