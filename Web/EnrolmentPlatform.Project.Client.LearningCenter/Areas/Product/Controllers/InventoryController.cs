using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Product.Controllers
{
    public class InventoryController : BaseController
    {
        /// <summary>
        /// 门票库存
        /// </summary>
        /// <returns></returns> 
        public ActionResult Ticket(Guid? id)
        {
            Guid TicketId = id ?? id.GetValueOrDefault();
            return View(TicketId);
        }

        /// <summary>
        /// 操作班期
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> OptionInventoryForTicket(OptionParamForTicketForDataForDateDto optionParamForTicketForDataForDateDto)
        {
            optionParamForTicketForDataForDateDto.SupplierId = this.SupplierId;
            optionParamForTicketForDataForDateDto.CreatorAccount = this.UserAccount;
            optionParamForTicketForDataForDateDto.CreatorUserId = this.UserId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Schedule/ModifyScheduleForTicketForSupplier",
                JsonConvert.SerializeObject(optionParamForTicketForDataForDateDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 操作班期
        /// </summary>
        /// <param name="searchParamForTicketForScheduleDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<JsonResult> SearchScheduleForTicket(SearchParamForTicketForScheduleDto searchParamForTicketForScheduleDto)
        {
            searchParamForTicketForScheduleDto.SupplierId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Schedule/SearchScheduleForTicketForSupplier",
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