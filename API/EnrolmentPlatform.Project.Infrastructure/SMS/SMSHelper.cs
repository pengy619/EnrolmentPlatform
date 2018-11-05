using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.SMS
{
    public class SMSHelper
    {
        /// <summary>
        /// 第一信息发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool SendSmsMessage(string mobile, string msg)
        {
            bool _result = false;
            string _name = ConfigurationManager.AppSettings["SMS_Account"].ToString();
            string _pwd = ConfigurationManager.AppSettings["SMS_Pwd"].ToString();
            string _sign = ConfigurationManager.AppSettings["SMS_Sign"].ToString();
            StringBuilder _arge = new StringBuilder();
            _arge.AppendFormat("name={0}", _name);
            _arge.AppendFormat("&pwd={0}", _pwd);
            _arge.AppendFormat("&content={0}", msg);
            _arge.AppendFormat("&mobile={0}", mobile);
            _arge.AppendFormat("&sign={0}", _sign);
            _arge.Append("&type=pt");
            string _weburl = ConfigurationManager.AppSettings["SMS_Url"].ToString();

            string _resp = HttpMethods.HttpPost(_weburl, _arge.ToString());
            if (_resp.Split(',')[0] == "0") //发送成功
            {
                _result = true;
            }

            return _result;
        }
        /// <summary>
        /// 容联。云通讯
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="msg">内容数据，需定义成数组方式，如模板中有两个参数，定义方式为{"1234","10"}</param>
        /// <param name="templateId">短信模板id</param>
        /// <returns></returns>
        public static string CCPSendSms(string mobile, string[] data, string templateId)
        {
            string appId = ConfigurationManager.AppSettings["SMS_AppId"].ToString();
            string accountsid = ConfigurationManager.AppSettings["SMS_AccountSid"].ToString();
            string authtoken = ConfigurationManager.AppSettings["SMS_Authtoken"].ToString();
            string ret = null;
            CCPRestSDK api = new CCPRestSDK();
            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount(accountsid, authtoken);
            api.setAppId(appId);
            try
            {
                if (isInit)
                {
                    Dictionary<string, object> retData = api.SendTemplateSMS(mobile, templateId, data);
                    ret = getDictionaryData(retData);
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            return ret;
        }
        private static string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }
    }
}
