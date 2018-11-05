using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using System.Net.Http;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Enums.Article;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Operate.Controllers
{
    public class SettingB2CController : BaseController
    {
        // GET: Operate/SettingB2C
        /// <summary>
        /// 全站配置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Website()
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/SystemBasicSetting/GetTotalStationSet", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                return View(msg.Data.ToString().ToObject<TotalStationSetDTO>());
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
        public async Task<ActionResult> WebsiteSet(TotalStationSetDTO dto)
        {
            dto.UpdateUserName = base.UserAccount;
            dto.UpdateUserId = base.UserId;
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/SystemBasicSetting/TotalStationSet", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }

        /// <summary>
        /// 首页
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
            req.PublicObject = (int)PublicObjectEnum.B2C;
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
            dto.PublicObject = (int)PublicObjectEnum.B2C;
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
        /// 农产品商城配置
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmMall(int? classify, int? position)
        {
            ViewBag.Classify = classify ?? (int)BannerClassifyEnum.SpecialtyList;
            ViewBag.Position = position ?? (int)BannerPositionEnum.SoldNow;

            //初始化广告设置
            var dto = new InitBannerSettingsDto
            {
                CreatorUserId = this.UserId,
                CreatorAccount = this.UserAccount
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>("/api/Banner/InitBannerSettings",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());

            return View();
        }

        /// <summary>
        /// 农产品商城Banner操作
        /// </summary>
        /// <returns></returns>
        public ActionResult FarmMallOption(BannerDto dto)
        {
            if (!dto.BannerId.IsEmpty())
            {
                dto = GetBannerInfo(dto.BannerId);
                return View(dto);
            }
            return View(dto);
        }

        /// <summary>
        /// 清除广告设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ClearBannerSettings(Guid bannerId)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Banner/ClearBannerSettings",
                JsonConvert.SerializeObject(bannerId),
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

        #region 游乐项目

        /// <summary>
        /// 游乐项目设置
        /// </summary>
        /// <returns></returns>
        public ActionResult RecommendAmusementProject()
        {

            return View();
        }
        /// <summary>
        /// 游乐项目设置操作
        /// </summary>
        /// <returns></returns>
        public ActionResult RecommendAmusementProjectOption()
        {
            string idStr = Request.QueryString["id"];
            ViewBag.OpreationStatus = 1;
            if (!string.IsNullOrEmpty(idStr))
            {
                //推荐信息
                var data = WebApiHelper.Get<HttpResponseMsg>(
                     "/api/RecommendAmusementProject/Find",
                     "", "id=" + idStr,
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                RecommendAmusementProjectDto info = data.Data.ToString().ToObject<RecommendAmusementProjectDto>();
                ViewBag.Info = info;

                //游乐项目信息
                var data2 = WebApiHelper.Post<HttpResponseMsg>(
                 "/api/TicketForPlayForSupplier/GetPlayInfoByIdForSupplier",
                 JsonConvert.SerializeObject(new ParamForSpecialtyForDetailDto() { ProductId = info.AmusementProjectId }),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
                ViewBag.ProjectInfo=data2.Data.ToString();

                //修改
                ViewBag.OpreationStatus = 2;
            }
            return View();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteRecommendAmusementProject(List<Guid> ids)
        {
            RecommendAmusementProjectDeleteDto dto = new RecommendAmusementProjectDeleteDto()
            {
                Classify = EnrolmentPlatform.Project.DTO.Enums.Product.RecommendPositionEnum.HomeForB2C,
                Ids = ids
            };
            var data = WebApiHelper.Post<HttpResponseMsg>("/api/RecommendAmusementProject/Delete",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess });
        }

        /// <summary>
        /// 排序调整
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SortRecommendAmusementProject(Guid id, int sortId)
        {
            //排序
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", id.ToString());
            parames.Add("sortId", sortId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);

            var data = WebApiHelper.Get<HttpResponseMsg>("/api/RecommendAmusementProject/Sort"
                , parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess });
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Save(RecommendAmusementProjectSaveDto dto)
        {
            dto.UserId = this.UserId;
            dto.Classify = RecommendPositionEnum.HomeForB2C;
            var data = WebApiHelper.Post<HttpResponseMsg>("/api/RecommendAmusementProject/Save",
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
                 "/api/RecommendAmusementProject/FindList",
                 "", "type=" + ((int)RecommendPositionEnum.HomeForB2C).ToString(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 获得游乐项目查询列表
        /// </summary>
        /// <param name="dto">查询实体</param>
        /// <returns></returns>
        public string TicketSearch(SearchParamForPlayMngDto dto)
        {
            var data = WebApiHelper.Post<HttpResponseMsg>(
                 "/api/TicketForPlayForSupplier/GetPlayListForSupplier",
                 JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        #endregion
    }
}