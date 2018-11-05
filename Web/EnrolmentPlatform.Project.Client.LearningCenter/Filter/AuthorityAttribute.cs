using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EnrolmentPlatform.Project.DTO.Systems;
using System.Linq;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Filter
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorityAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 获得对应的请求信息
            string areaName = filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            // 登陆校验
            var loginInfo = LoginInfoHandle.GetLoginInfo();
            if (loginInfo == null)
            {
                // 没有登陆
                filterContext.Result = new RedirectResult("/login/Index");
                return;
            }

            // 供应商主账户
            if (loginInfo.IsMaster == true)
            {
                //不需要检查
                return;
            }

            // 如果请求的是Base控制器
            if (controllerName == "base")
            {
                // 不做处理
                return;
            }

            // 不需要校验的权限
            if (LoginInfoHandle.NoCheckPermission.Exists(a => a.Area.ToLower() == areaName && a.Controller.ToLower() == controllerName &&
             a.Action.ToLower() == actionName) == true)
            {
                // 不做处理
                return;
            }

            // 获得所有用户权限（包含下面的操作权限）
            var permissionList = LoginInfoHandle.GetUserPermissions();
            if (permissionList == null || permissionList.Count == 0)
            {
                // 当前用户没有任何权限
                filterContext.Result = new RedirectResult("/Home/NoPermission");
                return;
            }

            if (permissionList.Exists(a => a.Area.ToLower() == areaName && a.Controller.ToLower() == controllerName &&
                    a.Action.ToLower() == actionName) == false)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                  
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("/Home/NoPermission");
                }
            }
        }
    }
}