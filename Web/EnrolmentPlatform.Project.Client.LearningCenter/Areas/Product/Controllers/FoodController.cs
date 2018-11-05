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
    public class FoodController : BaseController
    {
        /// <summary>
        /// 菜名设置
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index(FoodSearchDto req)
        {
            List<FoodListDto> list = new List<FoodListDto>();
            req.SupplierId = this.SupplierId;
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Food/GetFoodList",
                JsonConvert.SerializeObject(req),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (!ret.Data.IsEmpty())
            {
                list = ret.Data.ToString().ToList<FoodListDto>();
            }
            return View(list);
        }

        /// <summary>
        /// 菜名操作
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? id)
        {
            FoodEditDto dto = new FoodEditDto();
            if (id.HasValue)
            {
                dto = GetFoodInfo(id.Value);
            }
            return View(dto);
        }

        /// <summary>
        /// 保存菜名设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SaveFoodSetting(FoodEditDto dto)
        {
            dto.CreatorUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            dto.EnterpriseId = this.SupplierId;
            if (dto.FoodId.IsEmpty())
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Food/AddFood",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
            else
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Food/UpdateFood",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
        }

        /// <summary>
        /// 菜名详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid id)
        {
            FoodEditDto dto = GetFoodInfo(id);
            if (dto == null)
            {
                return RedirectToAction("Index", "Food");
            }
            return View(dto);
        }

        /// <summary>
        /// 删除菜名
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteFood(Guid foodId)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Food/DeleteFood",
                JsonConvert.SerializeObject(foodId),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 获取菜名详情
        /// </summary>
        /// <param name="foodId"></param>
        /// <returns></returns>
        private FoodEditDto GetFoodInfo(Guid foodId)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("foodId", foodId.ToString());
            param.Add("supplierId", this.SupplierId.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Food/GetFoodInfoById", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return ret.Data.ToString().ToObject<FoodEditDto>();
        }
    }
}