using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.DTO.Enums.Systems;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Account.Controllers
{
    public class SupplierController : BaseController
    {
        /// <summary>
        /// 供应商管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> SupplierList(SupplierSearchDto param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Enterprise/GetSupplierPageList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 启用/禁用供应商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdateSupplierStatus(UpdateEnterpriseStatusDto dto)
        {
            dto.OperatorId = this.UserId;
            dto.Operator = this.UserAccount;
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Enterprise/UpdateEnterpriseStatus",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteSuppliers(DeleteEnterpriseDto dto)
        {
            dto.OperatorId = this.UserId;
            dto.Operator = this.UserAccount;
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Enterprise/DeleteEnterprise",
                JsonConvert.SerializeObject(dto),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 供应商操作
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? supplierId)
        {
            EnterpriseAddDto dto = new EnterpriseAddDto();
            if (supplierId.HasValue)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("enterpriseId", supplierId.Value.ToString());
                Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
                var ret = WebApiHelper.Get<HttpResponseMsg>(
                    "/api/Enterprise/GetEnterpriseById", parameters.Item1, parameters.Item2,
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                if (ret.IsSuccess)
                {
                    dto = ret.Data.ToString().ToObject<EnterpriseAddDto>();
                }
            }
            return View(dto);
        }

        /// <summary>
        /// 保存供应商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> SaveSupplier(EnterpriseAddDto dto)
        {
            dto.Classify = SystemTypeEnum.TrainingInstitutions;
            dto.CurUserId = this.UserId;
            dto.CurUserAccount = this.UserAccount;
            if (dto.EnterpriseId.IsEmpty())
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Enterprise/AddEnterprise",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
            else
            {
                var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Enterprise/UpdateEnterpriseInfo",
                    JsonConvert.SerializeObject(dto),
                    ConfigurationManager.AppSettings["StaffId"].ToInt());
                return Json(ret);
            }
        }

        /// <summary>
        /// 重置供应商账号密码
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ResetPassword(Guid supplierId)
        {
            var ret = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Enterprise/ResetSupplierAccountPassword",
                JsonConvert.SerializeObject(supplierId),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(ret);
        }

        /// <summary>
        /// 配置报读学校
        /// </summary>
        /// <returns></returns>
        public ActionResult SchoolConfig(Guid enterpriseId)
        {
            //所有学校
            var schoolList = MetadataService.GetEnableList(DTO.Enums.Basics.MetadataTypeEnum.School).Select(t => new { value = t.Id, title = t.Name }).ToList();
            ViewBag.SchoolList = JsonConvert.SerializeObject(schoolList);
            //不可报考学校
            var notSchoolIds = EnterpriseService.GetNotSchoolIds(enterpriseId);
            ViewBag.SchoolIds = JsonConvert.SerializeObject(notSchoolIds);
            return View(enterpriseId);
        }

        /// <summary>
        /// 保存招生机构学校配置
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveConfig(SchoolSettingDto dto)
        {
            dto.CreatorUserId = this.UserId;
            var ret = EnterpriseService.SaveConfig(dto);
            return Json(ret);
        }
    }
}