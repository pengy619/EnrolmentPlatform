using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.SMS
{
    public static class MiaodiHelper
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phoneNO">短信接收端手机号码集合。用英文逗号分开，每批发送的手机号数量不得超过50000个</param>
        /// <param name="content">短信签名+短信内容</param>
        /// <returns></returns>
        public static bool SendSMS(string phoneNO, string content)
        {
            bool _result = false;
            if (!phoneNO.IsEmpty())
            {
                string _accountSid = ConfigurationManager.AppSettings["MD_AccountSID"];
                string _authToken = ConfigurationManager.AppSettings["MD_AUTHTOKEN"];
                string _restUrl = ConfigurationManager.AppSettings["MD_RestUrl"];

                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var sig = GetMD5(_accountSid + _authToken + timestamp);
                string sms = "accountSid=" + _accountSid + "&sig=" + sig + "&smsContent=" + content + "&to=" + phoneNO + "&timestamp=" + timestamp;

                string _resp = HttpMethods.HttpPost(_restUrl, sms);
                if (_resp != null)
                {
                    JObject jObj = JObject.Parse(_resp);
                    if (jObj["respCode"].ToString() == "00000") //发送成功
                    {
                        _result = true;
                    }
                }
            }
            return _result;
        }

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="sourceStr">待转换字符串</param>
        /// <returns>加密之后字符串</returns>
        private static string GetMD5(string sourceStr)
        {
            byte[] data = Encoding.GetEncoding("GB2312").GetBytes(sourceStr);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            // return OutString.ToUpper();
            return OutString.ToLower();
        }

    }
}