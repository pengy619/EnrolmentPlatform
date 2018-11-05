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
    public class PlayMngController : BaseController
    {
        // GET: Product/PlayMng 
        public ActionResult Index()
        {
            //状态
            ViewBag.PlayProjectStatus = EnumDescriptionHelper.GetItemValueList<StatusForPlayProjectEnum, int>().ToList();

            //景点范围
            ViewBag.ScenicRange = EnumDescriptionHelper.GetItemValueList<StatusForScenicRangeEnum, int>().ToList();
            return View();
        }

        /// <summary>
        /// 游玩项目列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> PlayList(SearchParamForPlayMngDto param)
        {
            param.SupplierId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketForPlayForSupplier/GetPlayListForSupplier",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 删除游玩项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeletePlay(string id)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("id", id);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var ret = await WebApiHelper.GetAsync<HttpResponseMsg>(
                "/api/TicketForPlayForSupplier/DeletePlay", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加游玩项目页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? id)
        {
            //景点范围
            ViewBag.ScenicRange = EnumDescriptionHelper.GetItemValueList<StatusForScenicRangeEnum, int>().ToList();

            OptionParamForPlayMngDataDto dto = new OptionParamForPlayMngDataDto();
            var _id = id ?? Guid.Empty;
            if (!_id.Equals(Guid.Empty))
            {
                dto = GetPlayInfo(_id);
            }
            return View(dto);
        }

        /// <summary>
        /// 供应商保存游玩项目
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SavePlay(OptionParamForPlayMngDataDto param)
        {
            param.SupplierId = this.SupplierId;
            param.CreatorUserId = this.UserId;
            param.CreateName = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/TicketForPlayForSupplier/SavePlayForSupplier",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res);
        }

        /// <summary>
        /// 供应商修改游玩项目
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ModifyPlay(OptionParamForPlayMngDataDto param)
        {
            param.SupplierId = this.SupplierId;
            param.CreatorUserId = this.UserId;
            param.CreateName = this.UserAccount;
            HttpResponseMsg res = await WebApiHelper.PostAsync<HttpResponseMsg>(
                                "/api/TicketForPlayForSupplier/ModifyPlayForSupplier",
                                JsonConvert.SerializeObject(param),
                               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(res);
        }

        /// <summary>
        /// 游玩项目详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid id)
        {
            var dto = GetPlayInfo(id);
            return View(dto);
        }

        /// <summary>
        /// 获取游玩项目详情
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private OptionParamForPlayMngDataDto GetPlayInfo(Guid id)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = id,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/TicketForPlayForSupplier/GetPlayInfoByIdForSupplier",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            OptionParamForPlayMngDataDto data = new OptionParamForPlayMngDataDto();
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<OptionParamForPlayMngDataDto>();
            }
            return data;
        }

        [ValidateInput(false)]
        public async Task<string> GetScenicSportForList(SearchParamForTicketDto param)
        {
            param.Status = (int)StatusForTikcetEnum.Enabled;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ScenicSport/GetScenicSportForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }


        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Status"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public async Task<ActionResult> DisEnable(List<Guid> ids)
        {
            return await UpdateStatus(ids, (int)StatusForPlayProjectEnum.DisEnable, "");
        }

        /// <summary>
        /// 操作更新数据
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Status"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        private async Task<ActionResult> UpdateStatus(List<Guid> ids, int Status, string Remark)
        {
            var param = new OptionParamForSpecialtyDto
            {
                ProductIds = ids,
                Remark = Remark,
                Status = Status,
                ModifyUserId = this.UserId
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketForPlayForSupplier/UpdateStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}