using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Sale.Controllers
{
    public class ProductForTicketController : BaseController
    {
        /// <summary>
        /// 票务销售列表模式
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var accountDetails = WebApiHelper.Get<HttpResponseMsg>("/api/TicketSales/GetTicketSalesParam", "", "", ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.TicketSalesParam = accountDetails.Data.ToString().ToObject<TicketSalesParamDto>();
            return View();
        }

        /// <summary>
        ///  获取票务销售列表  
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> GetProductForTicketSalesList(SearchParamForTicketSalesDto dto)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/TicketSales/GetProductForTicketSalesList", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();

        }

        /// <summary>
        /// 获取 某个票务 某个时间内月的价格
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetTicketPrice(Guid productid, DateTime date)
        {
            var data = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/TicketSales/GetTicketPrice", "", "productid=" + productid.ToString() + "&date=" + date.ToString("yyyy-MM-dd"), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Content("{\"data\":" + data.Data.ToString() + "}", "application/json");

        }

        /// <summary>
        /// 保存 票务订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ActionResult> SaveTicketOrder(SaveTicketOrderDTO dto)
        {
            dto.AccountId = base.UserId;
            dto.CreateForSystem = (int)SystemTypeEnum.TrainingInstitutions;
            dto.OrderSource = (int)OrderSourceEnum.TouristServiceCenter;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/OrderForTicket/SaveTicket", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data);
        }


        /// <summary>
        /// 订单支付
        /// </summary>
        public ActionResult OrderPay(Guid id)
        {
            ViewBag.UserId = base.UserId;
            var data = WebApiHelper.Get<HttpResponseMsg>("/api/OrderForTicket/ScenicGetTicketOrderByOrderId", "", "orderId=" + id.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
            OrderDetailForTicketDTO orderDetail = data.Data.ToString().ToObject<OrderDetailForTicketDTO>();
            return View(orderDetail);
        }
        
        /// <summary>
        /// 图片模式
        /// </summary>
        /// <returns></returns>
        public ActionResult ImageModel()
        {
            var accountDetails = WebApiHelper.Get<HttpResponseMsg>("/api/TicketSales/GetTicketSalesParam", "", "", ConfigurationManager.AppSettings["StaffId"].ToInt());
            ViewBag.TicketSalesParam = accountDetails.Data.ToString().ToObject<TicketSalesParamDto>();

            //查询列表
            SearchParamForTicketSalesDto dto = new SearchParamForTicketSalesDto();
            if (!string.IsNullOrEmpty(Request.QueryString["AmusementItem"]))
            {
                dto.AmusementItems = Request.QueryString["AmusementItem"].Split('_').ToList();
                ViewBag.AmusementItems = dto.AmusementItems;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["AttractionsId"]))
            {
                dto.AttractionsIds = Request.QueryString["AttractionsId"].Split('_').ToList();
                ViewBag.AttractionsIds = dto.AttractionsIds;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["productName"]))
            {
                dto.ProductName = Request.QueryString["productName"];
                ViewBag.ProductName = dto.ProductName;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["supplierName"]))
            {
                dto.SupplierName = Request.QueryString["supplierName"];
                ViewBag.SupplierName = dto.SupplierName;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["productCode"]))
            {
                dto.ProductNumber = Request.QueryString["productCode"];
                ViewBag.ProductNumber = dto.ProductNumber;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["supplierType"]))
            {
                dto.SupplierType = (SupplierTypeEnum)Convert.ToInt32(Request.QueryString["supplierType"]);
                ViewBag.SupplierType = Request.QueryString["supplierType"];
            }

            dto.Limit = 5;
            if (!string.IsNullOrEmpty(Request.QueryString["Limit"]))
            {
                dto.Limit = Convert.ToInt32(Request.QueryString["Limit"]);
            }
            dto.Page = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["Page"]))
            {
                dto.Page = Convert.ToInt32(Request.QueryString["Page"]);
            }
            int reCount = 0;
            var data = WebApiHelper.Post<HttpResponseMsg>("/api/TicketSales/GetProductForTicketSalesImageModelList", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            var res = data.Data.ToString().ToObject<GridDataResponse>();
            if (res.Data != null)
            {
                ViewBag.DataList = res.Data.ToString().ToObject<List<ImageTikectListDto>>();
                reCount = res.Count;
            }
            ViewBag.ReCount = reCount;
            ViewBag.Limit = dto.Limit;
            ViewBag.Page = dto.Page;
            return View();
        }
    }
}