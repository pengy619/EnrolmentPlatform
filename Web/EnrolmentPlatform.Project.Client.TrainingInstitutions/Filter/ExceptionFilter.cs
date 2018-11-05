using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Filter
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
}