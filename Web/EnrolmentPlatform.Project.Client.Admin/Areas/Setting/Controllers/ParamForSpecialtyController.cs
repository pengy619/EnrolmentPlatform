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
    public class ParamForSpecialtyController : BaseController
    {
        // GET: Setting/ParamForSpecialty
        /// <summary>
        /// 农产品参数
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region 产品分类
        /// <summary>
        /// 获取农产品分类列表
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> GetProductCategoriesForAdmin(SearchParamForCategoriesDto param)
        {
            param.Classify = (int)ProductClassifyEnum.Specialty;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductCategories/GetProductCategoriesForAdmin",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 获取农产品分类列表
        /// </summary>
        /// <returns></returns> 
        [HttpPost]
        public async Task<List<ProductCategoriesDto>> GetProductCategoriesForAllAdmin()
        {
            List<ProductCategoriesDto> list = new List<ProductCategoriesDto>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("cateClassify", ((int)ProductClassifyEnum.Specialty).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var ret = await WebApiHelper.GetAsync<HttpResponseMsg>(
                "/api/ProductCategories/GetProductCategoriesForAll", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductCategoriesDto>();
            }
            return list;
        }

        /// <summary>
        /// 获取农产品分类列表
        /// </summary>
        /// <returns></returns> 
        [HttpPost]
        public async Task<JsonResult> GetProductCategoriesForAll()
        {
            List<ProductCategoriesDto> dto = await GetProductCategoriesForAllAdmin();
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台添加分类
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddProductCategories(OptionParamForSpecialtyForAdminDto param)
        {
            param.Classify = (int)ProductClassifyEnum.Specialty;
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductCategories/AddProductCategoriesForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台修改分类
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
        /// 管理后台删除分类
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

        #region 农产品名称
        /// <summary>
        /// 获取农产品名称列表
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> GetSpecialtyNameForAdmin(GridDataRequest param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForSpecialtyName/GetSpecialtyNameForAdmin",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 管理后台添加农产品名称
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddSpecialtyName(OptionParamForSpecialtyForAdminDto param)
        {
            param.Classify = (int)ProductClassifyEnum.Specialty;
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductForSpecialtyName/AddSpecialtyNameForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台修改农产品名称
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ModifySpecialtyName(OptionParamForSpecialtyForAdminDto param)
        {
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductForSpecialtyName/ModifySpecialtyNameForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台删除农产品名称
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> DelSpecialtyName(Guid id)
        {
            OptionParamForSpecialtyDto optionParamForSpecialtyDto = new OptionParamForSpecialtyDto();
            var ids = new List<Guid>();
            ids.Add(id);
            optionParamForSpecialtyDto.ProductIds = ids;
            optionParamForSpecialtyDto.ModifyUserId = this.UserId;
            optionParamForSpecialtyDto.ModifyUserName = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductForSpecialtyName/DelSpecialtyNameForAdmin",
                                JsonConvert.SerializeObject(optionParamForSpecialtyDto),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 产品单位
        /// <summary>
        /// 获取产品单位列表
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> GetProductUnit(GridDataRequest param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForSpecialtyUnit/GetProductForSpecialtyUnitForAdmin",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 管理后台添加单位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddProductUnit(OptionParamForSpecialtyForAdminDto param)
        {
            param.Classify = (int)ProductClassifyEnum.Specialty;
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductForSpecialtyUnit/AddProductForSpecialtyUnitForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台修改单位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ModifyProductUnit(OptionParamForSpecialtyForAdminDto param)
        {
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductForSpecialtyUnit/ModifyProductForSpecialtyUnitForAdmin",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 管理后台删除单位
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> DelProductUnit(List<Guid> ids)
        {
            //optionParamForSpecialtyDto.ModifyUserId = this.UserId;
            //optionParamForSpecialtyDto.ModifyUserName = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/ProductForSpecialtyUnit/DelProductForSpecialtyUnitForAdmin",
                                JsonConvert.SerializeObject(ids),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}