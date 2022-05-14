using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.Infrastructure;
using System.Xml;
using EnrolmentPlatform.Project.DTO.Systems;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Basics;

namespace EnrolmentPlatform.Project.Client.Admin.Controllers
{
    
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得指标信息
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public string GetStockList(StockListSearchDto dto)
        {
            var res = StockSettingService.GetStockList(dto);
            return res.ToJson();
        }

        #region orgrion

        #region 

        public ActionResult SystemInfo()
        {
            //状态
            ViewBag.MessageStatus = EnumDescriptionHelper.GetItemValueList<MessageStatusEnum, int>().ToList();
            return View();
        }

        /// <summary>
        /// 没有权限
        /// </summary>
        /// <returns></returns>
        public ActionResult NoPermission()
        {
            return View();
        }

        /// <summary>
        /// 系统信息列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> SystemMessageList(ParamForSystemMessageDto param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SystemMessage/GetSystemMessageForSupplierForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 系统信息已读
        /// </summary>
        /// <param name="messageIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> MessageOnRead(List<Guid> messageIds)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/SystemMessage/MessageOnReadForSupplier",
                JsonConvert.SerializeObject(messageIds),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { Status = data.IsSuccess, Message = data.Info });
        }
        #endregion

        public async Task<string> LogSettingForTable(LogSettingDTO param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
             "/api/LogSetting/FindLogSettingByKeyForGridData",
             JsonConvert.SerializeObject(param),
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }
        public async Task<string> Search(LogSettingDTO param)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/LogSetting/FindLogSettingByKeyForGridData",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        [ValidateInput(false)]
        [HttpPost]
        public async Task<JsonResult> GetDataInfo(string startTime, string endTime)
        {
            if (startTime == endTime)
            {
                endTime = DateTime.Parse(endTime).AddDays(1).ToString("yyyy-MM-dd");
            }
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("startTime", startTime);
            param.Add("endTime", endTime);
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);
            var data = await WebApiHelper.GetAsync<HttpResponseMsg>(
            "/api/SystemMessage/GetHomeInfoForAdminDtoByTime", parameters.Item1, parameters.Item2,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            HomeInfoForAdminDto homeInfoForAdminDto = data.Data.ToString().ToObject<HomeInfoForAdminDto>();
            return Json(homeInfoForAdminDto, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用于管理员添加权限
        /// </summary>
        /// <returns></returns>
        public ActionResult DistributionPermissions()
        {
            return View();
        }

        /// <summary>
        /// 安全退去
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            EnrolmentPlatform.Project.Client.Admin.Filter.LoginInfoHandle.ClearCookie();

            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("roleId", base.RoleId.ToString());
            parames.Add("systemClassify", ((int)SystemTypeEnum.ChannelCenter).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);

            WebApiHelper.Get<HttpResponseMsg>(
                 "/api/Role/ClearRoleAndSystemPremission", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());

            return RedirectToAction("Index", "Login", new { area = "" });
        }

        [ChildActionOnly]
        public ActionResult NavigationMenu(string areaName, string controllerName, string action)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("area", areaName);
            param.Add("controller", controllerName);
            param.Add("action", action);
            param.Add("roleId", base.RoleId.ToString());
            param.Add("SystemTypeEnum", ((int)SystemTypeEnum.ChannelCenter).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(param);

            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetPermissionsByLocation", parameters.Item1, parameters.Item2,
           ConfigurationManager.AppSettings["StaffId"].ToInt());

            List<RolePermissionDto> rolePermissionDtoLst = data.Data.ToString().ToObject<List<RolePermissionDto>>();
            return PartialView("_navigationMenu", rolePermissionDtoLst);
        }

        /// <summary>
        /// 获得系统所有的权限
        /// </summary>
        /// <param name="systemType">系统类型</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSystemPermissionList(int systemType)
        {
            var data = WebApiHelper.Get<HttpResponseMsg>(
           "/api/Role/GetAllPermissionList", "", "systemTypeEnum=" + systemType,
           ConfigurationManager.AppSettings["StaffId"].ToInt());
            var list = data.Data.ToString().ToObject<List<RolePermissionDto>>();
            var firstLevelItem = list.Where(a => a.Level == 1).OrderBy(a => a.Sort).ToList();
            List<RolePermissionDto> result = new List<RolePermissionDto>();
            foreach (var item in firstLevelItem)
            {
                result.Add(item);

                //第二级
                var secondItem = list.Where(a => a.ParentId == item.Id).OrderBy(a => a.Sort).ToList();
                foreach (var item2 in secondItem)
                {
                    item2.Name = "--" + item2.Name;
                    result.Add(item2);

                    //第三级
                    var thirdItem = list.Where(a => a.ParentId == item2.Id).OrderBy(a => a.Sort).ToList();
                    foreach (var item3 in thirdItem)
                    {
                        item3.Name = "---" + item3.Name;
                        result.Add(item3);

                        //最后一级
                        var lastItem = list.Where(a => a.ParentId == item3.Id).OrderBy(a => a.Sort).ToList();
                        foreach (var item4 in lastItem)
                        {
                            item4.Name = "----" + item4.Name;
                            result.Add(item4);
                        }
                    }
                }
            }

            return Json(result);
        }

        /// <summary>
        /// 权限保存
        /// </summary>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult SavePermission(PermissionDto dto)
        {
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Role/SavePermission",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess });
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeletePermission(PermissionDto dto)
        {
            var data = WebApiHelper.Post<HttpResponseMsg>(
                "/api/Role/DeletePermission",
                JsonConvert.SerializeObject(dto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(new { ret = data.IsSuccess });
        }

        /// <summary>
        /// 查询权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPermission(Guid id)
        {
            var data = WebApiHelper.Get<HttpResponseMsg>(
                "/api/Role/GetPermission", "", "id=" + id.ToString(),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            var obj = data.Data.ToString().ToObject<PermissionDto>();
            return Json(obj);
        }

        #region  New Permissions
        public ActionResult Permissions()
        {
            
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetPermissionList(int classify)
        {
            List<JSTree> listtree = new List<JSTree>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("systemTypeEnum", classify.ToString());//平台
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/Role/GetAllPermissionList", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.IsSuccess)
            {
                List<RolePermissionDto> list = msg.Data == null ? null : msg.Data.ToString().ToObject<List<RolePermissionDto>>();
                InitJsTree(list, ref listtree);
            }
            return Json(listtree, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetPermissionById(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", id.ToString());//平台
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/Role/GetPermissionById", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            PermissionsDTO entity = null;
            if (msg.IsSuccess)
            {
                 entity = msg.Data == null ? null : msg.Data.ToString().ToObject<PermissionsDTO>();
                
            }
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> DeletePermissionById(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", id.ToString());//平台
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/Role/DeletePermissionById", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> UpdatePermission(PermissionsDTO entity)
        {
            var msg = await WebApiHelper.PostAsync<HttpResponseMsg>("/api/Role/UpdatePermission", JsonConvert.SerializeObject(entity), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
        #region 递归的得到树
        public void InitJsTree(List<RolePermissionDto> list, ref List<JSTree> listtree)
        {
            //增加根
            JSTree jstree = new JSTree();
            jstree.id = Guid.Empty;
            jstree.text = "权限";
            jstree.li_attr = new LiAttr() { level = 0 };
            jstree.state = new State() { opened = true };
            listtree.Add(jstree);
            //加载第一级模块数据
            if (list != null)
            {
                List<RolePermissionDto> level1 = list.Where(t => t.ParentId==Guid.Empty).ToList();
                if (level1.Any())
                {
                    List<JSTree> listchild = new List<JSTree>();
                    foreach (var item in level1)
                    {
                        JSTree jstreeitem = new JSTree();
                        jstreeitem.id = item.Id;
                        jstreeitem.text = item.Name;
                        //jstreeitem.icon = "";
                        jstreeitem.state = new State() { opened = false };
                        jstreeitem.li_attr = new LiAttr() { parentId = item.ParentId, level = item.Level };
                        jstreeitem.Sort = item.Sort;
                        listchild.Add(jstreeitem);
                    }
                    GetListTree(list, ref listchild);
                    listtree[0].children = listchild.OrderBy(t => t.Sort).ToList();
                }

            }


        }
        public void GetListTree(List<RolePermissionDto> list, ref List<JSTree> listtree)
        {
            foreach (var itemtree in listtree)
            {
                List<RolePermissionDto> listp = list.Where(t => t.ParentId == itemtree.id).ToList();
                List<JSTree> listchild = new List<JSTree>();
                foreach (var itemp in listp)
                {
                    JSTree jstreeitem = new JSTree();
                    jstreeitem.id = itemp.Id;
                    jstreeitem.text = itemp.Name;
                    jstreeitem.icon = itemp.Level == 4 ? "none" : "";
                    jstreeitem.state = new State() { opened = false };
                    jstreeitem.li_attr = new LiAttr() { parentId = itemp.ParentId, level = itemp.Level };
                    jstreeitem.Sort = itemp.Sort;
                    listchild.Add(jstreeitem);
                }
                GetListTree(list, ref listchild);
                itemtree.children = listchild.OrderBy(t => t.Sort).ToList();

            }

        }
        #endregion

        #endregion

        #endregion
    }
}