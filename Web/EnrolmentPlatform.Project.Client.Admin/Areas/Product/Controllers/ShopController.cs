using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers
{
    public class ShopController : BaseController
    {
        /// <summary>
        /// 店铺管理
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            //获得供应商店铺列表
            ShopSearchDto dto = new ShopSearchDto();
            if (!string.IsNullOrEmpty(Request.QueryString["ShopName"]))
            {
                dto.ShopName = Request.QueryString["ShopName"];
                ViewBag.ShopName = dto.ShopName;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["SupplierName"]))
            {
                dto.SupplierName = Request.QueryString["SupplierName"];
                ViewBag.SupplierName = dto.SupplierName;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
            {
                dto.Status = (EnrolmentPlatform.Project.DTO.Enums.Product.StatusForShopEnum)Convert.ToInt32(Request.QueryString["Status"]);
                ViewBag.Status = Convert.ToInt32(Request.QueryString["Status"]);
            }

            dto.Limit = 5;
            if (!string.IsNullOrEmpty(Request.QueryString["Limit"]))
            {
                dto.Limit = Convert.ToInt32(Request.QueryString["Limit"]);
            }
            dto.Page = 1;
            if (!string.IsNullOrEmpty(Request.QueryString["Page"]))
            {
                dto.Page = Convert.ToInt32(Request.QueryString["Page"]);
            }
            int reCount = 0;
            var data = WebApiHelper.Post<HttpResponseMsg>("/api/Shop/GetList", JsonConvert.SerializeObject(dto), ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data.Data != null)
            {
                ViewBag.DataList = data.Data.ToString().ToObject<List<ShopDto>>();
                reCount = int.Parse(data.Info);
            }
            ViewBag.ReCount = reCount;
            ViewBag.Limit = dto.Limit;
            ViewBag.Page = dto.Page;
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

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Audit(AuditDto dto)
        {
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Shop/Audit", dto.ToJson(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());

            return Json(new { ret = ret.IsSuccess });
        }
    }

    public class ShopListNameHelper
    {
        public static string GetOpenTimeName(ShopDto dto)
        {
            var openTime = "";
            if (dto != null)
            {
                if (dto.OpenTime == "0")
                {
                    openTime = "24小时营业";
                }
                else
                {
                    openTime = dto.OpenTime + "-" + dto.CloseTime;
                }
            }
            return openTime;
        }

        public static string GetOpenDayName(ShopDto dto)
        {
            var openDayStr = "";
            if (dto != null)
            {
                if (dto.OpenDay.Length == 7)
                {
                    openDayStr = "周一至周日";
                }
                else
                {
                    foreach (var item in dto.OpenDay)
                    {
                        if (item == '1')
                        {
                            openDayStr += "周一";
                        }
                        else if (item == '2')
                        {
                            openDayStr += "周二";
                        }
                        else if (item == '3')
                        {
                            openDayStr += "周三";
                        }
                        else if (item == '4')
                        {
                            openDayStr += "周四";
                        }
                        else if (item == '5')
                        {
                            openDayStr += "周五";
                        }
                        else if (item == '6')
                        {
                            openDayStr += "周六";
                        }
                        else if (item == '7')
                        {
                            openDayStr += "周日";
                        }
                    }
                }
            }

            return openDayStr;
        }
    }
}