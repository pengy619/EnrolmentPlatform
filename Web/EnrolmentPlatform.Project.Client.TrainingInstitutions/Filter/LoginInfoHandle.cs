using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Xml.Linq;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Filter
{
    /// <summary>
    /// 登陆信息处理
    /// </summary>
    public class LoginInfoHandle
    {
        #region 登陆信息

        /// <summary>
        /// 登陆cookie
        /// </summary>
        public static string LoginCookieName = "TouristCenterUserInfo";

        /// <summary>
        /// 获得登陆信息
        /// </summary>
        /// <returns></returns>
        public static UserLoginDto GetLoginInfo()
        {
            UserLoginDto loginInfo = null;
            string cookieValue = CookieHelper.GetCookieValue(LoginCookieName);
            if (!string.IsNullOrEmpty(cookieValue))
            {
                string val = DESEncrypt.Decrypt(cookieValue);
                loginInfo = val.ToObject<UserLoginDto>();
                //更新cookie过期时间
                CookieHelper.SetCookieValue(LoginCookieName, cookieValue, 10, 1);
            }
            if (loginInfo != null && loginInfo.UserId != Guid.Empty)
            {
                return loginInfo;
            }
            return null;
        }

        /// <summary>
        /// 设置登陆信息
        /// </summary>
        /// <returns></returns>
        public static void SetLoginInfo(UserLoginDto dto)
        {
            string cookieVal = dto.ToJson();
            string desCookieVal = DESEncrypt.Encrypt(cookieVal);
            CookieHelper.SetCookieValue(LoginCookieName, desCookieVal, 10, 1);
        }

        /// <summary>
        /// 移除cookie
        /// </summary>
        public static void ClearCookie()
        {
            CookieHelper.ClearCookie(LoginCookieName);
        }

        #endregion

        #region 用户权限

        // 不需要校验的权限列表
        public static List<RolePermissionDto> NoCheckPermission = new List<RolePermissionDto>()
        {
            new RolePermissionDto(){ Area="", Controller="home",Action= "index" },
            new RolePermissionDto(){ Area="", Controller="home",Action= "loginout" },
            new RolePermissionDto(){ Area="", Controller="home",Action= "nopermission" },
            new RolePermissionDto(){ Area="", Controller="home",Action= "navigationmenu" },
            new RolePermissionDto(){ Area="", Controller="base",Action= "getsecondmenu" }
        };

        /// <summary>
        /// 获得系统所有菜单权限
        /// </summary>
        /// <returns></returns>
        public static List<RolePermissionDto> GetAllPermissionList()
        {
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetAllPermissionList", "", "systemTypeEnum=" + (int)SystemTypeEnum.TrainingInstitutions,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString().ToObject<List<RolePermissionDto>>();
        }

        /// <summary>
        /// 获得用户菜单权限（包含操作权限）
        /// </summary>
        /// <returns></returns>
        public static List<RolePermissionDto> GetUserPermissions()
        {
            UserLoginDto user = GetLoginInfo();
            if (user == null)
            {
                return null;
            }

            //系统权限树权限（该权限包含操作权限）
            var data = WebApiHelper.Get<HttpResponseMsg>(
            "/api/Role/GetRolePermissionList", "", "roleId=" + user.RoleId.ToString() + "&systemTypeEnum=" + (int)SystemTypeEnum.TrainingInstitutions,
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            List<RolePermissionDto> list = data.Data.ToString().ToObject<List<RolePermissionDto>>();
            return list;
        }

        /// <summary>
        /// 根据当前请求路由获得模块ID
        /// </summary>
        /// <param name="routeData">请求路由</param>
        /// <returns>返回空默认是首页菜单</returns>
        public static Guid GetMoudleId(RouteData routeData)
        {
            var actionName = routeData.Values["action"].ToString().ToLower();
            var controllerName = routeData.Values["controller"].ToString().ToLower();
            var areaName = routeData.DataTokens["area"] != null ? routeData.DataTokens["area"].ToString().ToLower() : "";

            // 不需要校验的权限
            if (LoginInfoHandle.NoCheckPermission.Exists(a => a.Area.ToLower() == areaName && a.Controller.ToLower() == controllerName &&
             a.Action.ToLower() == actionName) == true)
            {
                // 直接返首页
                return Guid.Parse("C073E363-0784-43B1-BB47-F46EF4C21683");
            }

            List<RolePermissionDto> permissionList = GetAllPermissionList();

            // 查找操作权限
            var actionDto = permissionList.FirstOrDefault(a => a.Area.ToLower() == areaName && a.Controller.ToLower() == controllerName &&
              a.Action.ToLower() == actionName);

            // 否则查找父节点直至父节点为level = 1
            var firstItem = GetFirstLevel(1, permissionList, actionDto);
            if (firstItem == null)
            {
                // 直接返首页
                return Guid.Parse("C073E363-0784-43B1-BB47-F46EF4C21683");
            }

            return firstItem.Id;
        }

        /// <summary>
        /// 递归向上查找固定等级的模块ID
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="allList"></param>
        /// <param name="curItem"></param>
        /// <returns></returns>
        private static RolePermissionDto GetFirstLevel(int level, List<RolePermissionDto> allList, RolePermissionDto curItem)
        {
            if (curItem == null || curItem.Level == level)
            {
                return curItem;
            }

            curItem = allList.FirstOrDefault(a => a.Id == curItem.ParentId);
            return GetFirstLevel(level, allList, curItem);
        }

        /// <summary>
        /// 根据当前请求路由获得菜单ID
        /// </summary>
        /// <param name="routeData">请求路由</param>
        /// <returns>返回空默认是首页菜单</returns>
        public static Guid GetMenuId(RouteData routeData)
        {
            var actionName = routeData.Values["action"].ToString().ToLower();
            var controllerName = routeData.Values["controller"].ToString().ToLower();
            var areaName = routeData.DataTokens["area"] != null ? routeData.DataTokens["area"].ToString().ToLower() : "";

            // 不需要校验的权限
            if (LoginInfoHandle.NoCheckPermission.Exists(a => a.Area.ToLower() == areaName && a.Controller.ToLower() == controllerName &&
             a.Action.ToLower() == actionName) == true)
            {
                // 直接返首页
                return Guid.Parse("5E8A0889-3CC5-472D-8AF7-0EEBCE809FDC");
            }

            List<RolePermissionDto> permissionList = GetAllPermissionList();

            // 查找操作权限
            var actionDto = permissionList.FirstOrDefault(a => a.Area.ToLower() == areaName && a.Controller.ToLower() == controllerName &&
              a.Action.ToLower() == actionName);

            // 否则查找父节点直至父节点为level = 3
            var firstItem = GetFirstLevel(3, permissionList, actionDto);
            if (firstItem == null)
            {
                // 直接返首页
                return Guid.Parse("5E8A0889-3CC5-472D-8AF7-0EEBCE809FDC");
            }

            return firstItem.Id;
        }

        #endregion
    }
}