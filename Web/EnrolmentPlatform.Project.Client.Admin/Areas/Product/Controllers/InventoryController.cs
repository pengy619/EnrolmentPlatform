using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Product/Inventory
        public ActionResult Ticket(Guid? id)
        {
            Guid TicketId = id ?? id.GetValueOrDefault();
            return View(TicketId);
        }

        /// <summary>
        /// 操作班期
        /// </summary>
        /// <param name="searchParamForTicketForScheduleDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<JsonResult> SearchScheduleForTicket(SearchParamForTicketForScheduleDto searchParamForTicketForScheduleDto)
        { 
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Schedule/SearchScheduleForTicketForAdmin",
                JsonConvert.SerializeObject(searchParamForTicketForScheduleDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            List<SearchParamForTicketForScheduleForDataDto> list = new List<SearchParamForTicketForScheduleForDataDto>();
            if (data.IsSuccess)
            {
                list = data.Data.ToString().ToList<SearchParamForTicketForScheduleForDataDto>();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}