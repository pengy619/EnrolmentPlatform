using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.Client.LearningCenter.Filter;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Product.Controllers
{
    public class SpecialtyController : BaseController
    {
        // GET: Product/Specialty 
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
        /// 农产品列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> ProductList(SearchParamForSpecialtyDto param)
        {
            param.SupplierId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SpecialtyForSupplier/GetProductForSpecialtyForSupplierForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 农产品申请上架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> ProductOnSale(List<Guid> productIds)
        {
            return await UpdateProductStatus(productIds, (int)ProductStatusEnum.Checking);
        }

        /// <summary>
        /// 农产品下架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> ProductOffSale(List<Guid> productIds)
        {
            return await UpdateProductStatus(productIds, (int)ProductStatusEnum.OffSale);
        }

        private async Task<ActionResult> UpdateProductStatus(List<Guid> productIds, int status)
        {
            var param = new OptionParamForSpecialtyDto
            {
                ProductIds = productIds,
                Status = status,
                ModifyUserId = this.UserId,
                ProductClassify = (int)ProductClassifyEnum.Specialty
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/UpdateProductStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }


        /// <summary>
        /// 删除农产品
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteProduct(List<Guid> productIds)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SpecialtyForSupplier/DelProductForSpecialtyForSupplier",
                JsonConvert.SerializeObject(productIds),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }

        /// <summary>
        /// 复制农产品
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> CopyProduct(Guid productId)
        {
            var param = new ProductForCopyDto
            {
                ProductId = productId,
                SupplierId = this.SupplierId,
                Classify = (int)ProductClassifyEnum.Specialty,
                CreatorUserId = this.UserId,
                CreatorAccount = this.UserAccount
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/CpoyProductById",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }

        /// <summary>
        /// 农产品上市
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> ProductToMarket(Guid productId)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SpecialtyForSupplier/ModifyProductForSpecialtyForSupplierForIsMarket",
                JsonConvert.SerializeObject(productId),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }

        /// <summary>
        /// 添加农产品页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? productId)
        {
            //农产品分类
            ViewBag.Categories = GetProductCategories();

            //销售单位
            List<ProductForSpecialtyUnitDto> list = new List<ProductForSpecialtyUnitDto>();
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ProductForSpecialtyUnit/GetProductForSpecialtyUnitForSupplierAll", "", "",
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductForSpecialtyUnitDto>();
            }
            ViewBag.SalesUnits = list;

            OptionParamForSpecialtyForDataDto dto = new OptionParamForSpecialtyForDataDto();
            var _productId = productId ?? Guid.Empty;
            if (!_productId.Equals(Guid.Empty))
            {
                dto = GetProductInfo(productId.Value);
            }
            return View(dto);
        }

        /// <summary>
        /// 供应商修改农产品[基本设置]
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ModifyProductForBasicInfo(OptionParamForSpecialtyForDataDto param)
        {
            param.SupplierId = this.SupplierId;
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            param.Status = (int)ProductStatusEnum.Checking;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/SpecialtyForSupplier/ModifyProductForSpecialtyForSupplierForBasicInfo",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res);
        }

        /// <summary>
        /// 供应商修改农产品[销售设置]
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ModifyProductForSaleInfo(OptionParamForSpecialtyForDataDto param)
        {
            param.SupplierId = this.SupplierId;
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            param.Status = (int)ProductStatusEnum.Checking;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/SpecialtyForSupplier/ModifyProductForSpecialtyForSupplierForSaleInfo",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res);
        }


        /// <summary>
        /// 存为草稿
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SaveBasicInfoForDraft(OptionParamForSpecialtyForDataDto param)
        {
            param.SupplierId = this.SupplierId;
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            string apiUrl = "/api/SpecialtyForSupplier/AddProductForSpecialtyForDraftForSupplier";
            apiUrl = param.Id.Equals(Guid.Empty) ? apiUrl : "/api/SpecialtyForSupplier/ModifyProductForSpecialtyForSupplierForBasicInfo";
            param.Status = (int)ProductStatusEnum.Draft;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(apiUrl, JsonConvert.SerializeObject(param), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res);
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
        /// 获取产品详情
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private OptionParamForSpecialtyForDataDto GetProductInfo(Guid productId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = productId,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/SpecialtyForSupplier/GetProductForSpecialtyForSupplierByProductId",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            OptionParamForSpecialtyForDataDto data = new OptionParamForSpecialtyForDataDto();
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<OptionParamForSpecialtyForDataDto>();
            }
            return data;
        }

        /// <summary>
        /// 获取农产品分类列表
        /// </summary>
        /// <returns></returns>
        private List<ProductCategoriesDto> GetProductCategories()
        {
            List<ProductCategoriesDto> list = new List<ProductCategoriesDto>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("cateClassify", "1");
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
        /// 根据农产品分类和农产品名称获取品种
        /// </summary>
        /// <param name="categoriesId"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult GetSpecialtyVarieties(string categoriesId, string nameId)
        {
            List<ProductForSpecialtyVarietiesDto> list = new List<ProductForSpecialtyVarietiesDto>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("productCategoriesId", categoriesId);
            param.Add("specialtyNameId", nameId);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ProductForSpecialtyVarieties/GetProductForSpecialtyVarietiesForSupplierAll", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductForSpecialtyVarietiesDto>();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据品种获取农产品规格
        /// </summary>
        /// <param name="varietiesId"></param>
        /// <returns></returns>
        public ActionResult GetSpecialtyFormats(string varietiesId)
        {
            ListForProductForSpecialtyFormatDataForSupplierDto data = new ListForProductForSpecialtyFormatDataForSupplierDto();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("varietiesId", varietiesId);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ProductForSpecialtyFormat/GetSpecialtyForFormatForSupplierByVarietiesId", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<ListForProductForSpecialtyFormatDataForSupplierDto>();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加农产品规格
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> AddSpecialtyFormats(OptionParamForSpecialtyForFormatDto param)
        {
            param.CreatorUserId = this.UserId;
            param.CreatorAccount = this.UserAccount;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForSpecialtyFormat/AddForSpecialtyForFormatForSupplier",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }
    }
}