using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Attributes.API
{
    public class ApiSecurityForAdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            ResultMsg resultMsg = new ResultMsg();
            var request = actionContext.Request;
            string method = request.Method.Method;
            string staffid = String.Empty, timestamp = string.Empty, nonce = string.Empty, signature = string.Empty, staffkey = string.Empty;
            int id = 0;
            //httpclient预热会发送HEAD请求
            if (method.ToUpper().Equals("HEAD"))
            {
                return;
            }
            if (request.Headers.Contains("staffid"))
            {
                staffid = HttpUtility.UrlDecode(request.Headers.GetValues("staffid").FirstOrDefault());
            }
            if (request.Headers.Contains("staffkey"))
            {
                staffkey = HttpUtility.UrlDecode(request.Headers.GetValues("staffkey").FirstOrDefault());
            }
            if (request.Headers.Contains("timestamp"))
            {
                timestamp = HttpUtility.UrlDecode(request.Headers.GetValues("timestamp").FirstOrDefault());
            }
            if (request.Headers.Contains("nonce"))
            {
                nonce = HttpUtility.UrlDecode(request.Headers.GetValues("nonce").FirstOrDefault());
            }

            if (request.Headers.Contains("signature"))
            {
                signature = HttpUtility.UrlDecode(request.Headers.GetValues("signature").FirstOrDefault());
            }
            #region 判断请求头是否包含以下参数
            if (
                    string.IsNullOrEmpty(staffid) ||
                    !int.TryParse(staffid, out id) ||
                    string.IsNullOrEmpty(timestamp) ||
                    string.IsNullOrEmpty(nonce) ||
                    string.IsNullOrEmpty(staffkey)
                )//|| string.IsNullOrEmpty(signature)
            {
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.ParameterError;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.ParameterError);
                resultMsg.Data = "";
                resultMsg.IsSuccess = false;
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                base.OnActionExecuting(actionContext);
                return;
            }
            #endregion

            #region 得到管理员后台 staffId 与 staffKey
            string staffidforApi = ConfigurationManager.AppSettings["StaffId_1001"];
            string staffkeyforApi = ConfigurationManager.AppSettings["StaffKey_1001"];

            string staffidforSceneicApi = ConfigurationManager.AppSettings["StaffId_1006"];
            string staffkeyforSceneicApi = ConfigurationManager.AppSettings["StaffKey_1006"];

            if (
                !(staffidforApi.Equals(staffid)&& staffkeyforApi.Equals(staffkey))
                && !(staffidforSceneicApi.Equals(staffid) && staffkeyforSceneicApi.Equals(staffkey))
                
                )
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.Unauthorized;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.Unauthorized);
                resultMsg.Data = "";
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                base.OnActionExecuting(actionContext);
                return;
            }
            #endregion
            base.OnActionExecuting(actionContext);
        }
    }
}
