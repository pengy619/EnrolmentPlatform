using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Setting.Controllers
{
    public class DepartmentController : BaseController
    {
        // GET: Setting/Department
        /// <summary>
        /// 部门管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> Search(DepartmentSearchDto param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Department/GetDepartmentList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult SaveDepartment(DepartmentDto dto)
        {
            dto.CreateUserId = this.UserId;
            dto.CreatorAccount = this.UserAccount;
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Department/SaveDepartment",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess, msg = data.Info });
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult DeleteDepartment(Guid[] departmentIds)
        {
            DepartmentDeleteDto dto = new DepartmentDeleteDto()
            {
                DepartmentIds = departmentIds,
                CreateUserId = this.UserId,
                CreatorAccount = this.UserAccount
            };
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Department/DeleteDepartment",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (data.IsSuccess == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = data.Info });
            }
        }
    }
}