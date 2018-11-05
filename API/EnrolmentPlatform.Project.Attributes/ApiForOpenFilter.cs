using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.Infrastructure;
using Newtonsoft.Json;
using EnrolmentPlatform.Project.Domain.Entities;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;
using System.Threading;
using System.Net.Http;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Attributes.API
{
    public class ApiForOpenFilter : FilterAttribute, IAuthorizationFilter
    {
        private string EncryptKey = "WisdomOpen";
        public virtual Task<System.Net.Http.HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<System.Net.Http.HttpResponseMessage>> continuation)
        {
            ResultMsg resultMsg = new ResultMsg();
            var task = actionContext.Request.Content.ReadAsStreamAsync();
            string content = string.Empty; //加密内容
            using (System.IO.Stream sm = task.Result)
            {
                if (sm != null)
                {
                    sm.Seek(0, SeekOrigin.Begin);
                    int len = (int)sm.Length;
                    byte[] inputByts = new byte[len];
                    sm.Read(inputByts, 0, len);
                    sm.Close();
                    content = Encoding.UTF8.GetString(inputByts);
                }
            }
            string method = actionContext.Request.Method.Method;
            if (method.ToLower() != "post")
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.HttpMehtodError;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.HttpMehtodError);
                resultMsg.Data = "";
                resultMsg.IsSuccess = false;
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                return continuation();
            }

            WisdomRequest resquest = Json.ToObject<WisdomRequest>(content);
            string body = resquest.RequestBody;
            string userId = resquest._RequestHead.UserId;
            string userKey = resquest._RequestHead.UserKey;
            string sign = resquest._RequestHead.Sign;
            string signsafe = GetMD5Str(resquest._RequestHead.UserId + resquest._RequestHead.TimeStamp +
                 resquest._RequestHead.Version + resquest.RequestBody + resquest._RequestHead.UserKey);
            if (sign != signsafe)
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.HttpRequestError;
                resultMsg.Info = EnumDescriptionHelper.GetDescription(StatusCodeForApiEnum.HttpRequestError);
                resultMsg.Data = "";
                resultMsg.IsSuccess = false;
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                return continuation();
            }
            //判断用户名(user_id)和密码在数据中是否是有效状态


            bool isError = false;
            string errorInfo = isError ? "该用户不存在" : "";
            if (!isError)
            {

                errorInfo = isError ? "该用户已被删除" : "";
            }
            if (!isError)
            {

                errorInfo = isError ? "该用户已被禁用" : "";
            }
            if (!isError)
            {

                errorInfo = isError ? "该用户密码错误" : "";
            }
            if (isError)
            {
                resultMsg = new ResultMsg();
                resultMsg.StatusCode = (int)StatusCodeForApiEnum.Unauthorized;
                resultMsg.Info = errorInfo;
                resultMsg.Data = "";
                resultMsg.IsSuccess = false;
                actionContext.Response = resultMsg.ToJson().ResponseMessage();
                return continuation();
            }

            //填充参数json
            string jsonHeadStr = resquest._RequestHead.ToJson();
            string jsonBodyStr = Decrypt(body, EncryptKey);
            actionContext.Request.Properties["jsonHeadStr"] = jsonHeadStr;
            //actionContext.Request.Properties["jsonBodyStr"] = jsonBodyStr;
            if (actionContext.Request.Method == HttpMethod.Post)
            {
                actionContext.Request.Content = new StringContent(jsonBodyStr, Encoding.UTF8, "application/json");
            }

            return continuation();
        }
        #region    辅助类

        public static string GetMD5Str(string toCryString)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(toCryString))).Replace("-", "").ToLower();//asp是小写,把所有字符变小写
        }
        /// <summary>
        /// des加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        private static string Encrypt(string encryptString, string encryptKey)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                byte[] byteEncrypt = Encoding.UTF8.GetBytes(encryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(Encoding.UTF8.GetBytes(encryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncrypt, 0, byteEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            return returnValue;
        }
        /// <summary>
        /// des解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptString, string decryptKey)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                byte[] byteDecryptString = Convert.FromBase64String(decryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(Encoding.UTF8.GetBytes(decryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            return returnValue;
        }

        #endregion
    }

    public class WisdomRequest
    {
        [JsonProperty("requestHead")]
        public RequestHead _RequestHead { get; set; }
        [JsonProperty("requestBody")]
        public string RequestBody { get; set; }

        public WisdomRequest()
        {
            this._RequestHead = new RequestHead();
        }
    }

    public class RequestHead
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("user_key")]
        public string UserKey { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
