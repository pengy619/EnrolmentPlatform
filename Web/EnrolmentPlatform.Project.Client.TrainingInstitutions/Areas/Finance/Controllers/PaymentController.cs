using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Finance.Controllers
{
    public class PaymentController : BaseController
    {



        /// <summary>
        /// 账户资产
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var accountDetails = WebApiHelper.Get<HttpResponseMsg>("/api/FinanceCenter/GetTouristsAccountDetails", "", "" , ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.TouristsAccountDetails = accountDetails.Data.ToString().ToObject<TouristsAccountDetailsDto>();
            return View();
        }
        /// <summary>
        ///  获取 门票 交易明细（游客服务中心）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> GetTransactionDetails(SearchTransactionDetailsDto dto)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/FinanceCenter/GetTransactionDetails", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }


    }
}