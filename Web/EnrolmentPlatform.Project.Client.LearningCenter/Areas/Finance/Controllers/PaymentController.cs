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
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Finance.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Finance/Payment
        /// <summary>
        /// 账户资产
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var SupplierBalance = WebApiHelper.Get<HttpResponseMsg>("/api/ApplyCash/GetSupplierBalance", "", "id=" + this.SupplierId.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.SupplierBalance = SupplierBalance.Data.ToString().ToObject<SupplierBalanceDto>();
            return View();
        }

        /// <summary>
        /// 获取结算单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetOrderSettlement(OrderSettlementRequestDto request)
        {
            request.EnterpriseId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/FinanceCenter/GetOrderSettlementList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 获取 企业资金交易流水 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetAccountDetailInfoList(AccountDetailInfoListDto request)
        {
            request.EnterpriseId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/FinanceCenter/GetAccountDetailInfoList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 获取未结算订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetPendingOrderInfoList(PendingOrderInfoRequestDto request)
        {
            request.EnterpriseId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/FinanceCenter/GetPendingOrderInfoList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 结算中心 
        /// </summary>
        /// <returns></returns>
        public ActionResult SettlementCenter()
        {
            var SettlementCenterInfo = WebApiHelper.Get<HttpResponseMsg>("/api/FinanceCenter/GetSettlementCenterInfo", "", "enterpriseId=" + this.SupplierId.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.SettlementCenterInfo = SettlementCenterInfo.Data.ToString().ToObject<SettlementCenterDto>();
            return View();
        }

        public ActionResult SettlementsDetails (Guid id)
        {
            var settlementsDetails = WebApiHelper.Get<HttpResponseMsg>("/api/FinanceCenter/GetSettlementsDetails", "", "id=" + id.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            SettlementsDetailsDto SettlementsDetails = settlementsDetails.Data.ToString().ToObject<SettlementsDetailsDto>();
            return View(SettlementsDetails);
        }



    }
}