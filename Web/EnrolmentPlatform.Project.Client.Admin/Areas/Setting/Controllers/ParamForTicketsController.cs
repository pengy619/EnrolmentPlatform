using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Setting.Controllers
{
    public class ParamForTicketsController : BaseController
    {
        // GET: Setting/ParamForTickets
        public ActionResult Index()
        {
            return View();
        }

        #region 票种
        /// <summary>
        /// 获取票种列表
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> GetProductCategoriesForAdmin(SearchParamForCategoriesDto param)
        {
            param.Classify = (int)ProductClassifyEnum.Ticket;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductCategories/GetProductCategoriesForAdmin",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }
         

        /// <summary>
        /// 管理后台添加票种
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddProductCategories(OptionParamForSpecialtyForAdminDto param)
        {
            param.Classify = (int)ProductClassifyEnum.Ticket;
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductCategories/AddProductCategoriesForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台修改票种
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ModifyProductCategories(OptionParamForSpecialtyForAdminDto param)
        {
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductCategories/ModifyProductCategoriesForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台删除票种
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> DelProductCategories(Guid id)
        {
            OptionParamForSpecialtyDto optionParamForSpecialtyDto = new OptionParamForSpecialtyDto();
            var ids = new List<Guid>();
            ids.Add(id);
            optionParamForSpecialtyDto.ProductIds = ids;
            optionParamForSpecialtyDto.ModifyUserId = this.UserId;
            optionParamForSpecialtyDto.ModifyUserName = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductCategories/DelProductCategoriesForAdmin",
                                JsonConvert.SerializeObject(optionParamForSpecialtyDto),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 票务参数
        /// <summary>
        /// 获取票种列表
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> GetTicketParamForAdmin(SearchParamForCategoriesDto param)
        { 
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketParam/GetTikcetParamForAdmin",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }


        /// <summary>
        /// 管理后台添加票种
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddTicketParam(OptionParamForSpecialtyForAdminDto param)
        { 
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/TicketParam/AddTikcetParamForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台修改票种
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ModifyTicketParam(OptionParamForSpecialtyForAdminDto param)
        {
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/TicketParam/ModifyTikcetParamForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台删除票种
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> DelTicketParam(Guid id)
        {
            OptionParamForSpecialtyDto optionParamForSpecialtyDto = new OptionParamForSpecialtyDto();
            var ids = new List<Guid>();
            ids.Add(id);
            optionParamForSpecialtyDto.ProductIds = ids;
            optionParamForSpecialtyDto.ModifyUserId = this.UserId;
            optionParamForSpecialtyDto.ModifyUserName = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/TicketParam/DelTikcetParamForAdmin",
                                JsonConvert.SerializeObject(optionParamForSpecialtyDto),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}