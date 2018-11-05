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
using EnrolmentPlatform.Project.DTO.Enums.Article;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Operate.Controllers
{
    public class SettingH5Controller : BaseController
    {
        /// <summary>
        /// H5全站配置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Website()
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/SystemBasicSetting/GetH5TitleSet", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                ViewBag.H5TitleSet = msg.Data;
                return View();
            }
            else
            {
                throw new Exception(msg.Info);
            }
        }
        /// <summary>
        /// 设置全站配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ActionResult> H5TitleSet(H5TitleSetDTO dto)
        {
            dto.UpdateUserName = base.UserAccount;
            dto.UpdateUserId = base.UserId;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/SystemBasicSetting/H5TitleSet", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }

        /// <summary>
        /// H5首页配置
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 广告列表
        /// </summary>
        /// <returns></returns>
        public async Task<string> BannerList(BannerSearchDto req)
        {
            req.PublicObject = (int)PublicObjectEnum.H5;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Banner/GetBannerPageList",
                JsonConvert.SerializeObject(req),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 首页Banner操作
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexOption(Guid? id)
        {
            BannerDto dto = new BannerDto();
            if (id.HasValue)
            {
                dto = GetBannerInfo(id.Value);
            }
            return View(dto);
        }

        /// <summary>
        /// 保存广告
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SaveBanner(BannerDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            dto.PublicObject = (int)PublicObjectEnum.H5;
            if (dto.BannerId.IsEmpty())
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Banner/AddBanner",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
            else
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Banner/UpdateBanner",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
        }

        /// <summary>
        /// 更新广告排序
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> UpdateBannerSort(UpdateBannerSortDto dto)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Banner/UpdateBannerSort",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteBanners(List<Guid> idList)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Banner/DeleteBanners",
                JsonConvert.SerializeObject(idList),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 获取广告详情
        /// </summary>
        /// <param name="bannerId"></param>
        /// <returns></returns>
        private BannerDto GetBannerInfo(Guid bannerId)
        {
            BannerDto dto = new BannerDto();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("id", bannerId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Banner/GetBannerById", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                dto = ret.Data.ToString().ToObject<BannerDto>();
            }
            return dto;
        }

        /// <summary>
        /// 农产品商城配置
        /// </summary>
        /// <returns></returns>
        public ActionResult SpecialtyShopMall()
        {

            return View();
        }

        /// <summary>
        /// 农产品分类设置操作
        /// </summary>
        /// <returns></returns>
        public ActionResult CategoryOpt()
        {
            string idStr = Request.QueryString["id"];
            ViewBag.OpreationStatus = 1;
            if (!string.IsNullOrEmpty(idStr))
            {
                //推荐信息
                var data = WebApiHelper.Get<HttpResponseMsg>(
                     "/api/RecommendProductCategories/Find",
                     "", "id=" + idStr,
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                RecommendProductCategoriesDto info = data.Data.ToString().ToObject<RecommendProductCategoriesDto>();
                ViewBag.Info = info;

                //修改
                ViewBag.OpreationStatus = 2;
            }
            return View();
        }

        /// <summary>
        /// 推荐农产品
        /// </summary>
        /// <returns></returns>
        public ActionResult RecommendAmusementProduct()
        {
            return View();
        }


        /// <summary>
        ///  获取推荐农产品 分页数据  
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetRecommendProductList(GridDataRequest request)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/RecommendProduct/GetPageList", JsonConvert.SerializeObject(request), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 推荐农产品操作
        /// </summary>
        /// <returns></returns>
        public ActionResult RecommendAmusementProductOpt(Guid? id)
        {
            RecommendProductDto model = new RecommendProductDto();
            if (id.HasValue && !id.IsEmpty())
            {
                var RecommendProduct = WebApiHelper.Get<HttpResponseMsg>("/api/RecommendProduct/GetRecommendProductById", "", "id=" + id.Value.ToString(), ConfigurationManager.AppSettings["StaffId"].ToInt());
                model = RecommendProduct.Data.ToString().ToObject<RecommendProductDto>();
            }
            return View(model);
        }


        /// <summary>
        /// 获取 农产品 分页列表，排除已推荐的
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<string> GetSpecialtyProductPageList(SpecialtyProductPageRequestDto dto)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/RecommendProduct/GetSpecialtyProductPageList", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 新增/编辑农产品操作
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrEditRecommendProduct(RecommendProductDto dto)
        {
            dto.CreatorUserId = UserId;
            dto.CreatorAccount = UserAccount;
            dto.Classify = EnrolmentPlatform.Project.DTO.Enums.Product.RecommendPositionEnum.SpecialtyShopMallForH5;
            var ret = WebApiHelper.Post<HttpResponseMsg>("/api/RecommendProduct/AddOrEditRecommendProduct", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 删除 推荐农产品操作
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRecommendProduct(DeleteRecommendProductDto dto)
        {
            dto.OperatorId = UserId;
            dto.Operator = UserAccount;
            var ret = WebApiHelper.Post<HttpResponseMsg>("/api/RecommendProduct/DeleteRecommendProduct", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }


        #region 农产品分类

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteRecommendProductCategories(List<Guid> ids)
        {
            RecommendProductCategoriesDeleteDto dto = new RecommendProductCategoriesDeleteDto()
            {
                Classify = EnrolmentPlatform.Project.DTO.Enums.Product.RecommendPositionEnum.SpecialtyShopMallForH5,
                Ids = ids
            };
            var data = WebApiHelper.Post<HttpResponseMsg>("/api/RecommendProductCategories/Delete",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess });
        }

        /// <summary>
        /// 排序调整
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SortRecommendProductCategories(Guid id, int sortId)
        {
            //排序
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", id.ToString());
            parames.Add("sortId", sortId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);

            var data = WebApiHelper.Get<HttpResponseMsg>("/api/RecommendProductCategories/Sort"
                , parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(RecommendProductCategoriesSaveDto dto)
        {
            dto.UserId = this.UserId;
            dto.Classify = EnrolmentPlatform.Project.DTO.Enums.Product.RecommendPositionEnum.SpecialtyShopMallForH5;
            var data = WebApiHelper.Post<HttpResponseMsg>("/api/RecommendProductCategories/Save",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess, info = data.Info });
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public string Search()
        {
            var data = WebApiHelper.Get<HttpResponseMsg>(
                 "/api/RecommendProductCategories/FindList",
                 "", "type=" + ((int)EnrolmentPlatform.Project.DTO.Enums.Product.RecommendPositionEnum.SpecialtyShopMallForH5).ToString(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 获得产品分类列表
        /// </summary>
        /// <param name="dto">查询实体</param>
        /// <returns></returns>
        [HttpGet]
        public string ProductCategoriesSearch()
        {
            var data = WebApiHelper.Get<HttpResponseMsg>(
                 "/api/ProductCategories/GetProductCategoriesForAll", "", "cateClassify=1",
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            List<ProductCategoriesDto> list = data.Data.ToString().ToObject<List<ProductCategoriesDto>>();
            GridDataResponse res = new GridDataResponse();
            res.Count = list.Count;
            res.Data = list;
            return res.ToJson();
        }

        #endregion
    }
}