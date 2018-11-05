using System;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Filter
{
    public class ExceptionFilter : HandleErrorAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                LogFactory.GetLogger(this.GetType()).Error(filterContext.Exception);
                HttpException httpEx = filterContext.Exception as HttpException;
                filterContext.ExceptionHandled = true;

                string errorMessage = filterContext.Exception.Message;
                errorMessage = filterContext.Exception.Message.Replace("\r\n", "\\r\\n").Replace("'", "\\'");
                filterContext.Result = new ContentResult
                {
                    Content = "<script language='javascript'>alert('" + errorMessage + "');history.go(-1);</script>",
                    ContentEncoding = System.Text.Encoding.UTF8,
                    ContentType = ""

                };
            }
        }
    }

    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            base.OnResultExecuted(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Buffer = true;
            filterContext.HttpContext.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            filterContext.HttpContext.Response.Expires = 0;
            filterContext.HttpContext.Response.CacheControl = "no-cache";
            filterContext.HttpContext.Response.Cache.SetNoStore();
            base.OnActionExecuted(filterContext);
        }
    }
}