using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers
{
    public class SpecialtyController : BaseController
    {
        #region 农产品
        /// <summary>
        /// 农产品 管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //农产品分类
            ViewBag.Categories = GetProductCategories();

            //产品状态
            ViewBag.ProductStatus = EnumDescriptionHelper.GetItemValueList<ProductStatusEnum, int>().ToList();

            //销售模式
            ViewBag.SaleModels = EnumDescriptionHelper.GetItemValueList<ProductSaleModelEnum, int>().ToList();
            return View();
        }
        /// <summary>
        /// 农产品详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid productId)
        {
            var dto = GetProductInfo(productId);
            return View(dto);
        }
        /// <summary>
        /// 农产品列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> ProductList(SearchParamForSpecialtyDto param)
        {
            param.StatusForNo = (int)ProductStatusEnum.Draft;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SpecialtyForAdmin/GetProductForSpecialtyForAdminForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 农产品审核上架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> ProductOnSale(List<Guid> productIds, int Status, string Remark)
        {
            //10代表强制上架
            return await UpdateProductStatus(productIds, Status.Equals(1) ? (int)ProductStatusEnum.OnSale : Status.Equals(2) ? (int)ProductStatusEnum.CheckNo : 10, Remark);
        }

        /// <summary>
        /// 农产品下架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> ProductOffSale(List<Guid> productIds)
        {
            return await UpdateProductStatus(productIds, (int)ProductStatusEnum.OffSale, "");
        }

        /// <summary>
        /// 操作更新数据
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="Status"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        private async Task<ActionResult> UpdateProductStatus(List<Guid> productIds, int Status, string Remark)
        {
            var param = new OptionParamForSpecialtyDto
            {
                ProductIds = productIds,
                Remark = Remark,
                Status = Status,
                ModifyUserId = this.UserId,
                ProductClassify = (int)ProductClassifyEnum.Specialty
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/UpdateProductStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除农产品
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteProduct(List<Guid> productIds)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SpecialtyForAdmin/DelProductForSpecialtyForAdmin",
                JsonConvert.SerializeObject(productIds),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取产品详情
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private OptionParamForSpecialtyForDataDto GetProductInfo(Guid productId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = productId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/SpecialtyForAdmin/GetProductForSpecialtyForAdminByProductId",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            OptionParamForSpecialtyForDataDto data = new OptionParamForSpecialtyForDataDto();
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<OptionParamForSpecialtyForDataDto>();
            }
            return data;
        }

        #endregion

        #region 产品品种
        /// <summary>
        /// 产品规格管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Varieties()
        {
            //农产品分类
            ViewBag.Categories = GetProductCategories();
            //状态
            ViewBag.StatusBase = EnumDescriptionHelper.GetItemValueList<StatusBaseEnum, int>().ToList();
            return View();
        }

        /// <summary>
        /// 获取农产品分类列表
        /// </summary>
        /// <returns></returns>
        private List<ProductCategoriesDto> GetProductCategories()
        {
            List<ProductCategoriesDto> list = new List<ProductCategoriesDto>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("cateClassify", ((int)ProductClassifyEnum.Specialty).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ProductCategories/GetProductCategoriesForAll", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductCategoriesDto>();
            }
            return list;
        }

        /// <summary>
        /// 根据农产品分类获取农产品名称
        /// </summary>
        /// <param name="categoriesId"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult GetSpecialtyNames(string categoriesId)
        {
            List<ProductForSpecialtyNameDto> list = new List<ProductForSpecialtyNameDto>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productCategoriesId", categoriesId);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ProductForSpecialtyName/GetSpecialtyNameForSupplierAll", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductForSpecialtyNameDto>();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 产品规格管理添加修改操作
        /// </summary>
        /// <returns></returns>
        public ActionResult VarietiesOpt(Guid? Id)
        {
            //农产品分类
            ViewBag.Categories = GetProductCategories();

            Guid varietiesId = Id.GetValueOrDefault();
            ProductForSpecialtyVarietiesForAdminDto productForSpecialtyVarietiesForAdminDto = new ProductForSpecialtyVarietiesForAdminDto();
            if (!varietiesId.Equals(Guid.Empty))
            {
                productForSpecialtyVarietiesForAdminDto = GetVarietiesInfo(varietiesId);
            }
            return View(productForSpecialtyVarietiesForAdminDto);
        }

        /// <summary>
        /// 根据varietiesId获取信息
        /// </summary>
        /// <param name="varietiesId"></param>
        /// <returns></returns>
        private ProductForSpecialtyVarietiesForAdminDto GetVarietiesInfo(Guid varietiesId)
        {
            ProductForSpecialtyVarietiesForAdminDto data = new ProductForSpecialtyVarietiesForAdminDto();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("varietiesId", varietiesId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ProductForSpecialtyFormat/GetSpecialtyForFormatForAdminByVarietiesId", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<ProductForSpecialtyVarietiesForAdminDto>();
            }
            return data;
        }

        /// <summary>
        /// 操作品种，有id则修改，否则添加
        /// </summary>
        /// <param name="productForSpecialtyVarietiesForAdminDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult OptionBasicInfo(ProductForSpecialtyVarietiesForAdminDto productForSpecialtyVarietiesForAdminDto)
        {
            productForSpecialtyVarietiesForAdminDto.CreatorAccount = this.UserAccount;
            productForSpecialtyVarietiesForAdminDto.CreatorUserId = this.UserId;
            if (productForSpecialtyVarietiesForAdminDto.VarietiesId.Equals(Guid.Empty))
            {
                var data = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/ProductForSpecialtyVarieties/AddForSpecialtyForVarietiesForAdmin",
                    JsonConvert.SerializeObject(productForSpecialtyVarietiesForAdminDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = WebApiHelper.Post<HttpResponseMsg>(
                       "/api/ProductForSpecialtyVarieties/ModifyForSpecialtyForVarietiesForAdmin",
                       JsonConvert.SerializeObject(productForSpecialtyVarietiesForAdminDto),
                      ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 管理后台删除品种
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> DelSpecialtyVarieties(Guid varietiesId)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("varietiesId", varietiesId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var ret = await WebApiHelper.GetAsync<HttpResponseMsg>(
                "/api/ProductForSpecialtyVarieties/DelForSpecialtyForVarietiesForAdmin", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 品种列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> VarietiesList(ProductSpecialtyVarietiesForQueryForAdminDto param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForSpecialtyVarieties/GetProductForSpecialtyVarietiesForAdminAll",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 管理后台修改品种状态
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> VarietiesOnEnabled(List<Guid> varietiesIds)
        {
            return await UpdateVarietiesStatus(varietiesIds, (int)StatusBaseEnum.Enabled);
        }

        /// <summary>
        /// 管理后台修改品种状态
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> VarietiesOnDisabled(List<Guid> varietiesIds)
        {
            return await UpdateVarietiesStatus(varietiesIds, (int)StatusBaseEnum.Disabled);
        }

        /// <summary>
        /// 管理后台修改品种状态
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        private async Task<JsonResult> UpdateVarietiesStatus(List<Guid> varietiesIds, int Status)
        {
            OptionParamForSpecialtyDto optionParamForSpecialtyDto = new OptionParamForSpecialtyDto();
            optionParamForSpecialtyDto.ProductIds = varietiesIds;
            optionParamForSpecialtyDto.Status = Status;
            optionParamForSpecialtyDto.ModifyUserId = this.UserId;
            optionParamForSpecialtyDto.ModifyUserName = this.UserAccount;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                    "/api/ProductForSpecialtyVarieties/UpdateVarietiesStatus",
                    JsonConvert.SerializeObject(optionParamForSpecialtyDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}