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
    public class PaymentController : BaseController
    {
        /// <summary>
        /// 财务资产
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var AccountAssets = WebApiHelper.Get<HttpResponseMsg>("/api/FinanceCenter/GetAccountAssets", "", "", ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.AccountAssets = AccountAssets.Data.ToString().ToObject<AccountAssetsDto>();

            return View();
        }

        /// <summary>
        /// 获取 资金交易流水 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetAccountDetailInfoList(AccountDetailInfoListDto request)
        {            
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/FinanceCenter/GetAccountDetailInfoList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 结算中心
        /// </summary>
        /// <returns></returns>
        public ActionResult SettlementCenter()
        {

            var SettlementCenterInfo = WebApiHelper.Get<HttpResponseMsg>("/api/FinanceCenter/GetSettlementCenterInfo", "", "enterpriseId="+Guid.Empty.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.SettlementCenterInfo = SettlementCenterInfo.Data.ToString().ToObject<SettlementCenterDto>();
            return View();
        }


        /// <summary>
        /// 获取未结算订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetPendingOrderInfoList(PendingOrderInfoRequestDto request)
        {            
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/FinanceCenter/GetPendingOrderInfoList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 获取结算单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetOrderSettlement(OrderSettlementRequestDto request)
        {            
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/FinanceCenter/GetOrderSettlementList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }


        /// <summary>
        /// 结算详情
        /// </summary>
        /// <returns></returns>
        public ActionResult SettlementCenterInfo(Guid id)
        {
            var settlementsDetails = WebApiHelper.Get<HttpResponseMsg>("/api/FinanceCenter/GetSettlementsDetails", "", "id=" + id.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            SettlementsDetailsDto SettlementsDetails = settlementsDetails.Data.ToString().ToObject<SettlementsDetailsDto>();
            return View(SettlementsDetails);
        }
    }
}