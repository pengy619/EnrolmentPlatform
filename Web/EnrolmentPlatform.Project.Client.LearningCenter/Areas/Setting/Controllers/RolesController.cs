using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Setting.Controllers
{
    public class RolesController : BaseController
    {
        // GET: Setting/Roles
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 角色详情界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? roleId)
        {
            ViewBag.OPStatus = 1;
            //获得所有权限列表
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetAllPermissionList", "", "systemTypeEnum=" + ((int)SystemTypeEnum.LearningCenter).ToString(),
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            var list = data.Data.ToString().ToObject<List<RolePermissionDto>>();

            //第一级为模块
            StringBuilder sb = new StringBuilder("[");
            var moduleList = list.Where(a => a.Level == 1).ToList();
            for (int i = 0; i < moduleList.Count; i++)
            {
                //一级模块组装
                var item = moduleList[i];
                sb.Append("{ title: \"" + item.Name + "\", value: \"" + item.Id + "\", data: [");

                //第二级为二级菜单
                var menuList = list.Where(a => a.ParentId == item.Id).ToList();
                for (int j = 0; j < menuList.Count; j++)
                {
                    //二级菜单组装
                    var item2 = menuList[j];
                    sb.Append("{ title: \"" + item2.Name + "\", value: \"" + item2.Id + "\", data: [");

                    //目前固定三级，只当第二级为Level2的时候才会有第三层级
                    if (item2.Level == 2)
                    {
                        var pageList = list.Where(a => a.ParentId == item2.Id).ToList();
                        for (int k = 0; k < pageList.Count; k++)
                        {
                            var item3 = pageList[k];
                            sb.Append("{ title: \"" + item3.Name + "\", value: \"" + item3.Id + "\", data: []}");
                            if (k != pageList.Count - 1)
                            {
                                sb.Append(",");
                            }
                        }
                    }

                    sb.Append("]}");
                    if (j != menuList.Count - 1)
                    {
                        sb.Append(",");
                    }
                }

                sb.Append("]}");
                if (i != moduleList.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]");
            ViewBag.PermissionList = sb.ToString();

            //如果是修改
            if (roleId.HasValue)
            {
                var data2 = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Role/GetRole", "", "roleId=" + roleId.Value.ToString(),
                ConfigurationManager.AppSettings["StaffId"].ToInt());
                ViewBag.OPStatus = 2;
                ViewBag.RoleInfo = data2.Data.ToString().ToObject<RoleDto>();
            }

            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> Search(RoleSearchDto param)
        {
            param.EnterpriseId = this.SupplierId;
            param.SystemType = EnrolmentPlatform.Project.DTO.Enums.Systems.SystemTypeEnum.LearningCenter;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/Role/GetRoleList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 角色保存
        /// </summary>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult SaveRole(RoleDto dto)
        {
            dto.EnterpriseId = this.SupplierId;
            dto.SystemType = EnrolmentPlatform.Project.DTO.Enums.Systems.SystemTypeEnum.LearningCenter;
            dto.CurUserId = this.UserId;
            dto.CurUserAccount = this.UserAccount;
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Role/SaveRole",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess, msg = data.Info });
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult DeleteRole(Guid [] roleIds)
        {
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Role/DeleteRole",
                JsonConvert.SerializeObject(roleIds),
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