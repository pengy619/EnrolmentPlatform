/*******************************************************************************
 * Author: SPF
 * Description: HttpMethods
*********************************************************************************/
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace EnrolmentPlatform.Project.Infrastructure
{
    /// <summary>
    /// 网络操作
    /// </summary>
    public class Net
    {
        #region Ip(获取Ip)
        /// <summary>
        /// 获取Ip
        /// </summary>
        public static string Ip
        {
            get
            {
                var result = string.Empty;
                if (HttpContext.Current != null)
                    result = GetWebClientIp();
                if (result.IsEmpty())
                    result = GetLanIp();
                return result;
            }
        }

        /// <summary>
        /// 获取Web客户端的Ip
        /// </summary>
        private static string GetWebClientIp()
        {
            var ip = GetWebRemoteIp();
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取Web远程Ip
        /// </summary>
        private static string GetWebRemoteIp()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    return hostAddress.ToString();
            }
            return string.Empty;
        }

        #endregion




        #region Host(获取主机名)

        /// <summary>
        /// 获取主机名
        /// </summary>
        public static string Host
        {
            get
            {
                return HttpContext.Current == null ? Dns.GetHostName() : GetWebClientHostName();
            }
        }

        /// <summary>
        /// 获取Web客户端主机名
        /// </summary>
        private static string GetWebClientHostName()
        {
            if (!HttpContext.Current.Request.IsLocal)
                return string.Empty;
            var ip = GetWebRemoteIp();
            var result = Dns.GetHostEntry(IPAddress.Parse(ip)).HostName;
            if (result == "localhost.localdomain")
                result = Dns.GetHostName();
            return result;
        }

        #endregion

        #region 获取mac地址
        /// <summary>
        /// 返回描述本地计算机上的网络接口的对象(网络接口也称为网络适配器)。
        /// </summary>
        /// <returns></returns>
        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }
        ///<summary>
        /// 通过NetworkInterface读取网卡Mac
        ///</summary>
        ///<returns></returns>
        public static List<string> GetMacByNetworkInterface()
        {
            List<string> macs = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                macs.Add(ni.GetPhysicalAddress().ToString());
            }
            return macs;
        }
        #endregion

        #region Ip城市(获取Ip城市)
        /// <summary>
        /// 获取IP地址信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetLocation(string ip)
        {
            string res = "";
            try
            {
                string url = "http://apis.juhe.cn/ip/ip2addr?ip=" + ip + "&dtype=json&key=b39857e36bee7a305d55cdb113a9d725";
                res = HttpMethods.HttpGet(url);
                var resjson = res.ToObject<objex>();
                res = resjson.result.area + " " + resjson.result.location;
            }
            catch
            {
                res = "";
            }
            if (!string.IsNullOrEmpty(res))
            {
                return res;
            }
            try
            {
                string url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=" + ip + "&resource_id=6006&ie=utf8&oe=gbk&format=json";
                res = HttpMethods.HttpGet(url, Encoding.GetEncoding("GBK"));
                var resjson = res.ToObject<obj>();
                res = resjson.data[0].location;
            }
            catch
            {
                res = "";
            }
            return res;
        }
        /// <summary>
        /// 百度接口
        /// </summary>
        public class obj
        {
            public List<dataone> data { get; set; }
        }
        public class dataone
        {
            public string location { get; set; }
        }
        /// <summary>
        /// 聚合数据
        /// </summary>
        public class objex
        {
            public string resultcode { get; set; }
            public dataoneex result { get; set; }
            public string reason { get; set; }
            public string error_code { get; set; }
        }
        public class dataoneex
        {
            public string area { get; set; }
            public string location { get; set; }
        }
        #endregion

        #region Browser(获取浏览器信息)
        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        public static string Browser
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var browser = HttpContext.Current.Request.Browser;
                return string.Format("{0} {1}", browser.Browser, browser.Version);
            }
        }
        #endregion

        #region 获取URL
        /// <summary>
        /// 获取URL信息
        /// </summary>
        public static string URL
        {
            get
            {
                if (HttpContext.Current == null &&HttpContext.Current.Request.Url.IsEmpty())
                    return string.Empty;
                var url = HttpContext.Current.Request.Url.ToString();
                return url;
            }
        }
        /// <summary>
        /// 获取原始URL信息
        /// </summary>
        public static string RawUrl
        {
            get
            {
                if (HttpContext.Current == null && HttpContext.Current.Request.RawUrl.IsEmpty())
                    return string.Empty;
                var rawUrl = HttpContext.Current.Request.RawUrl.ToString();
                return rawUrl;
            }
        }
        /// <summary>
        /// 获取上一次URL信息
        /// </summary>
        public static string UrlReferrer
        {
            get
            {
                if (HttpContext.Current == null && HttpContext.Current.Request.UrlReferrer.IsEmpty())
                    return string.Empty;
                var urlReferrer = HttpContext.Current.Request.UrlReferrer.ToString();
                return urlReferrer;
            }
        }
        #endregion
    }

    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic owinContext = request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }
    }
}
