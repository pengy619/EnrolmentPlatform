using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers
{
    public class LandMngController : BaseController
    {
        // GET: Product/LandMng
        public ActionResult Index()
        {
            //状态
            ViewBag.ProductStatus = EnumDescriptionHelper.GetItemValueList<StatusForTikcetEnum, int>().ToList();
            //景点等级
            ViewBag.TikcetParamLevel = GetTikcetParam((int)TikcetParamClassifyEnum.Level);
            //景点类型
            ViewBag.TikcetParamCate = GetTikcetParam((int)TikcetParamClassifyEnum.Cate);
            return View();
        }


        public ActionResult Option(Guid? scenicSportId)
        {
            //景点等级
            ViewBag.TikcetParamLevel = GetTikcetParam((int)TikcetParamClassifyEnum.Level);
            //景点类型
            ViewBag.TikcetParamCate = GetTikcetParam((int)TikcetParamClassifyEnum.Cate);

            OptionForTicketForScenicSportDataDto dto = new OptionForTicketForScenicSportDataDto();
            var _scenicSportId = scenicSportId ?? Guid.Empty;
            if (!_scenicSportId.Equals(Guid.Empty))
            {
                dto = GetScenicSportByScenicSportId(_scenicSportId);
            }
            return View(dto);
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <returns></returns>
        private List<ProductCategoriesDto> GetTikcetParam(int classify)
        {
            List<ProductCategoriesDto> list = new List<ProductCategoriesDto>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("classify", classify.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/TicketParam/GetTikcetParamForAll", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductCategoriesDto>();
            }
            return list;
        }

        /// <summary>
        /// 根据条件分页查询景点列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> GetScenicSportForList(SearchParamForTicketDto param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ScenicSport/GetScenicSportForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 存为草稿
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> SaveScenicSportForDraft(OptionForTicketForScenicSportDataDto param)
        {
            param.Status = (int)StatusForTikcetEnum.Draft;
            return await SaveScenicSport(param);
        }

        /// <summary>
        /// 存为启用
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> SaveScenicSportForEnabled(OptionForTicketForScenicSportDataDto param)
        {
            param.Status = (int)StatusForTikcetEnum.Enabled;
            return await SaveScenicSport(param);
        }

        private async Task<JsonResult> SaveScenicSport(OptionForTicketForScenicSportDataDto param)
        {
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            string apiUrl = "/api/ScenicSport/AddScenicSport";
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(apiUrl, JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<JsonResult> OnEnabled(List<Guid> ids)
        {
            return await ModifyScenicSportForStatus(ids, (int)StatusForTikcetEnum.Enabled);
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<JsonResult> OnDisabled(List<Guid> ids)
        {
            return await ModifyScenicSportForStatus(ids, (int)StatusForTikcetEnum.Disabled);
        }

        /// <summary>
        /// 删除景点
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns> 
        public async Task<JsonResult> DelScenicSport(Guid id)
        {
            OptionParamForSpecialtyDto optionParamForSpecialtyDto = new OptionParamForSpecialtyDto();
            optionParamForSpecialtyDto.ProductIds = new List<Guid>();
            optionParamForSpecialtyDto.ProductIds.Add(id);
            optionParamForSpecialtyDto.ModifyUserId = this.UserId;
            optionParamForSpecialtyDto.ModifyUserName = this.UserAccount;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ScenicSport/DelScenicSport",
                JsonConvert.SerializeObject(optionParamForSpecialtyDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改景点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> ModifyScenicSportForDraft(OptionForTicketForScenicSportDataDto param)
        {
            param.Status = (int)StatusForTikcetEnum.Draft;
            return await ModifyScenicSport(param);
        }

        /// <summary>
        /// 修改景点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> ModifyScenicSportForEnabled(OptionForTicketForScenicSportDataDto param)
        {
            param.Status = (int)StatusForTikcetEnum.Enabled;
            return await ModifyScenicSport(param);
        }

        private async Task<JsonResult> ModifyScenicSport(OptionForTicketForScenicSportDataDto param)
        {
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ScenicSport/ModifyScenicSport",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 景点状态更新
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private async Task<JsonResult> ModifyScenicSportForStatus(List<Guid> ids, int status)
        {
            OptionParamForSpecialtyDto data = new OptionParamForSpecialtyDto();
            data.Status = status;
            data.ProductIds = new List<Guid>();
            data.ProductIds = ids;
            data.ModifyUserId = this.UserId;
            data.ModifyUserName = this.UserAccount;
            string apiUrl = "/api/ScenicSport/ModifyScenicSportForStatus";
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(apiUrl, JsonConvert.SerializeObject(data), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据景点id查询数据
        /// </summary>
        /// <param name="scenicSportId"></param>
        /// <returns></returns>
        private OptionForTicketForScenicSportDataDto GetScenicSportByScenicSportId(Guid scenicSportId)
        {
            OptionForTicketForScenicSportDataDto data = new OptionForTicketForScenicSportDataDto();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("scenicSportId", scenicSportId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ScenicSport/GetScenicSportByScenicSportId", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<OptionForTicketForScenicSportDataDto>();
            }
            return data;
        }
    }
}