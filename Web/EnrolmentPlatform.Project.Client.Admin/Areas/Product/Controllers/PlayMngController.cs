using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers
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

            //供应商类型
            ViewBag.SupplierType = EnumDescriptionHelper.GetItemValueList<SupplierTypeEnum, int>().ToList();
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
        /// 审核
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Status"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public async Task<ActionResult> AuditPlay(List<Guid> ids, int Status, string Remark)
        {
            return await UpdateStatus(ids, Status == 1 ? (int)StatusForPlayProjectEnum.Enable : (int)StatusForPlayProjectEnum.CheckNo, Remark);
        }

        /// <summary>
        /// 强制启用10
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Status"></param> 
        /// <returns></returns>
        public async Task<ActionResult> Enable(List<Guid> ids)
        {
            return await UpdateStatus(ids, 10, "管理后台强制启用");
        }

        /// <summary>
        /// 强制禁用11
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Status"></param> 
        /// <returns></returns>
        public async Task<ActionResult> DisEnable(List<Guid> ids)
        {
            return await UpdateStatus(ids, 11, "管理后台强制禁用");
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
                "/api/TicketForPlayForAdmin/UpdateStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
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
        /// <param name="id"></param>
        /// <returns></returns>
        private OptionParamForPlayMngDataDto GetPlayInfo(Guid id)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = id
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
    }
}