using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using EnrolmentPlatform.Project.Infrastructure;
using System.Collections.Specialized;
using System.IO;
using EnrolmentPlatform.Project.DTO.Enums;
using System.Configuration;
using System.Web.Http;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using System.Runtime.Remoting.Messaging;

namespace EnrolmentPlatform.Project.Attributes.API
{
    public class ApiSecurityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)   // 允许匿名访问
            {
                base.OnActionExecuting(actionContext);
                return;
            }

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
            if (request.Headers.Contains("IP"))
            {
                string ip = HttpUtility.UrlDecode(request.Headers.GetValues("IP").FirstOrDefault());
                RedisHelper.Set("CustormIP", ip);
            }
            #region 对请求进行验证
            if (!method.ToUpper().Equals("POST")
                && !method.ToUpper().Equals("DELETE")
                && !method.ToUpper().Equals("GET")
                && !method.ToUpper().Equals("PUT")
                && !method.ToUpper().Equals("OPTIONS")
                )
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.HttpMehtodError;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.HttpMehtodError);
                resultMsg.Data = "";
                resultMsg.IsSuccess = false;
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                base.OnActionExecuting(actionContext);
            }

       
            #endregion

            #region 判断请求头是否包含以下参数
            if (
                    string.IsNullOrEmpty(staffid) ||
                    !int.TryParse(staffid, out id) ||
                    string.IsNullOrEmpty(timestamp) ||
                    string.IsNullOrEmpty(nonce) ||
                    string.IsNullOrEmpty(staffkey)
                )
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

            #region 校验客户端 与服务端 staffId 与 staffKey
            string staffidforApi = ConfigurationManager.AppSettings["StaffId_" + staffid];
            string staffkeyforApi = ConfigurationManager.AppSettings["StaffKey_" + staffid];
            if (string.IsNullOrEmpty(staffidforApi) || string.IsNullOrEmpty(staffkeyforApi))
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.Unauthorized;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.Unauthorized);
                resultMsg.Data = "";
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                base.OnActionExecuting(actionContext);
                return;
            }
            if (!staffidforApi.Equals(staffid) || !staffkeyforApi.Equals(staffkey))
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

            #region  判断timespan是否有效
            double ts1 = 0;
            double ts2 = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            bool timespanvalidate = double.TryParse(timestamp, out ts1);
            double ts = ts2 - ts1;
            bool falg = ts > ConfigurationManager.AppSettings["UrlExpireTime"].ToInt() * 1000;
            if (falg || (!timespanvalidate))
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.URLExpireError;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.URLExpireError);
                resultMsg.Data = "";
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                base.OnActionExecuting(actionContext);
                return;
            }
            #endregion

            #region GetToken方法不需要进行签名验证
            if (actionContext.ActionDescriptor.ActionName == "GetToken")
            {
                if (string.IsNullOrEmpty(staffid) || string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce))
                {
                    resultMsg.StatusCode = (int)StatusCodeForApiEnum.ParameterError;
                    resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.ParameterError);
                    resultMsg.IsSuccess = false;
                    resultMsg.Data = "";
                    actionContext.Response = resultMsg.ToJson().ResponseMessage();
                    base.OnActionExecuting(actionContext);
                    return;
                }
                else
                {
                    base.OnActionExecuting(actionContext);
                    return;
                }
            }


            #endregion

            #region 判断token是否有效
            Token token = RedisHelper.Get<Token>(id.ToString());
            string signtoken = string.Empty;
            if (token == null)
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.TokenInvalid;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.TokenInvalid);
                resultMsg.Data = "";
                resultMsg.IsSuccess = false;
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                base.OnActionExecuting(actionContext);
                return;
            }
            else
            {
                signtoken = token.SignToken.ToString();
                
            }
            #endregion

            #region 判断签名是否有效
            bool result = Validate(timestamp, nonce, id, token.SignToken.ToString(), signature);
            if (!result)
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.HttpRequestError;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.HttpRequestError);
                resultMsg.Data = "";
                resultMsg.IsSuccess = false;
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                base.OnActionExecuting(actionContext);
                return;
            }
            #endregion
            base.OnActionExecuting(actionContext);

        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="timeStamp">发起请求时的时间戳（单位：毫秒）</param>
        /// <param name="nonce">随机数</param>
        /// <param name="staffId">应用编号</param>
        /// <param name="token">token</param>
        /// <param name="signature">请求时签名</param>
        /// <returns>true/false</returns>
        private bool Validate(string timeStamp, string nonce, int staffId, string token, string signature)
        {

            var hash = System.Security.Cryptography.MD5.Create();
            //拼接签名数据
            var signStr = timeStamp + nonce + staffId + token;
            //使用MD5加密
            var result = Md5.Md5Hash(signStr);

            return result.ToString().ToUpper().Equals(signature.ToUpper());

        }
        //不需要验证的  

    }



}
