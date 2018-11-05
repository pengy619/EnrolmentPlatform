using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using EnrolmentPlatform.Project.Infrastructure;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity.Infrastructure;

namespace EnrolmentPlatform.Project.Attributes.API
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string msg = string.Empty;
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
                  actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message +
                  "——堆栈信息：" + actionExecutedContext.Exception.StackTrace+actionExecutedContext.Exception.Source;

            if (actionExecutedContext.Exception.InnerException != null)
            {
                msg +="actionExecutedContext.Exception.InnerException.Message"+
                       actionExecutedContext.Exception.InnerException.Message +
                      "——堆栈信息：" + actionExecutedContext.Exception.InnerException.StackTrace + actionExecutedContext.Exception.InnerException.Source;
            }

            LogFactory.GetLogger(this.GetType()).Error(msg);
            
            ResultMsg resultMsg = new ResultMsg
            {
                IsSuccess=false,
                Info = actionExecutedContext.Exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            if (actionExecutedContext.Exception is NotImplementedException)
            {
                resultMsg.StatusCode = (int)HttpStatusCode.NotImplemented;
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                resultMsg.StatusCode = (int)HttpStatusCode.RequestTimeout;
            }
            else if (actionExecutedContext.Exception is DbUpdateConcurrencyException)
            {
                resultMsg.Info = "提交失败！存在并发风险，请刷新后操作！";
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse<ResultMsg>(HttpStatusCode.OK, resultMsg);
            base.OnException(actionExecutedContext);
        }
  
    }
}
