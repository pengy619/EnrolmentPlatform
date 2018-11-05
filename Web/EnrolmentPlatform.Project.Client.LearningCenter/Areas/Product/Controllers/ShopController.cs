using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Product.Controllers
{
    public class ShopController : BaseController
    {
        /// <summary>
        /// 店铺管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获得当前供应商的店铺（目前只有一个，以后有可能是列表）
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("supplierId", this.SupplierId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Shop/GetShop", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data.Data != null)
            {
                ViewBag.Shop = data.Data.ToString().ToObject<ShopDto>();
            }
            return View();
        }

        /// <summary>
        /// 店铺新增/编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Option()
        {
            //获得菜系列表
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("classify", CateringParamClassifyEnum.FoodSeries.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/CateringParam/GetCateringParamList", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data.Data != null)
            {
                ViewBag.FoodSeries = data.Data.ToString().ToObject<List<CateringParamDto>>();
            }

            //获得服务设施列表
            Dictionary<string, string> parames2 = new Dictionary<string, string>();
            parames2.Add("classify", CateringParamClassifyEnum.ServiceFacilities.ToString());
            Tuple<string, string> parameters2 = WebApiHelper.GetQueryString(parames2);
            var data2 = WebApiHelper.Get<HttpResponseMsg>(
            "/api/CateringParam/GetCateringParamList", parameters2.Item1, parameters2.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data2.Data != null)
            {
                ViewBag.ServiceFacilities = data2.Data.ToString().ToObject<List<CateringParamDto>>();
            }

            //编辑
            if (!string.IsNullOrWhiteSpace(Request.QueryString["shopId"]))
            {
                Dictionary<string, string> parames3 = new Dictionary<string, string>();
                parames3.Add("shopId", Request.QueryString["shopId"]);
                Tuple<string, string> parameters3 = WebApiHelper.GetQueryString(parames3);
                var data3 = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Shop/GetShopById", parameters3.Item1, parameters3.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
                if (data3.Data != null)
                {
                    ViewBag.Shop = data3.Data.ToString().ToObject<ShopDto>();
                }
            }

            return View();
        }
        
        /// <summary>
        /// 店铺详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid shopId)
        {
            //获得服务设施列表
            Dictionary<string, string> parames2 = new Dictionary<string, string>();
            parames2.Add("classify", CateringParamClassifyEnum.ServiceFacilities.ToString());
            Tuple<string, string> parameters2 = WebApiHelper.GetQueryString(parames2);
            var data2 = WebApiHelper.Get<HttpResponseMsg>(
            "/api/CateringParam/GetCateringParamList", parameters2.Item1, parameters2.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data2.Data != null)
            {
                ViewBag.ServiceFacilities = data2.Data.ToString().ToObject<List<CateringParamDto>>();
            }

            Dictionary<string, string> parames3 = new Dictionary<string, string>();
            parames3.Add("shopId", shopId.ToString());
            Tuple<string, string> parameters3 = WebApiHelper.GetQueryString(parames3);
            var data3 = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Shop/GetShopById", parameters3.Item1, parameters3.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data3.Data != null)
            {
                ViewBag.Shop = data3.Data.ToString().ToObject<ShopDto>();
            }
            return View();
        }

        /// <summary>
        /// 保存店铺
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveShop(ShopDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            dto.EnterpriseId = this.SupplierId;
            dto.Classify = ProductClassifyEnum.Catering;
            dto.EnterpriseName = "";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("enterpriseId", dto.EnterpriseId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Enterprise/GetEnterpriseById", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                var supplierInfo = ret.Data.ToString().ToObject<EnterpriseAddDto>();
                dto.EnterpriseName = supplierInfo.EnterpriseName;
            }

            //保存
            var ret2 = WebApiHelper.Post<HttpResponseMsg>("/api/Shop/SaveShop",
                dto.ToJson(), ConfigurationManager.AppSettings["StaffId"].ToInt());

            //保存成功
            if (ret.IsSuccess == true)
            {
                return Json(new { ret = true });
            }
            return Json(new { ret = false, msg = ret.Info });
        }

        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteShopList(List<Guid> shopId)
        {
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Shop/DeleteShopList", shopId.ToJson(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            
            return Json(new { ret = ret.IsSuccess });
        }

        /// <summary>
        /// 停业
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UnemployedList(List<Guid> shopIdList)
        {
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Shop/UnemployedList", shopIdList.ToJson(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());

            return Json(new { ret = ret.IsSuccess });
        }

        /// <summary>
        /// 开业
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult OpeningList(List<Guid> shopIdList)
        {
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Shop/OpeningList", shopIdList.ToJson(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());

            return Json(new { ret = ret.IsSuccess });
        }
    }
}