using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Threading;
using System.Linq.Expressions;
using System.Reflection;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public class CookieHelper
    {
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">时间</param>
        /// <param name="TimeType">1,分钟 2,小时 3,天</param>
        public static void SetCookieValue(string key, string value, int expiration, int TimeType = 1)
        {
            HttpCookie cookie = new HttpCookie(key);
            cookie = new HttpCookie(key, value);
            switch (TimeType)
            {
                case 1:
                    cookie.Expires = DateTime.Now.AddMinutes(Convert.ToDouble(expiration));
                    break;
                case 2:
                    cookie.Expires = DateTime.Now.AddHours(Convert.ToDouble(expiration));
                    break;
                case 3:
                    cookie.Expires = DateTime.Now.AddDays(Convert.ToDouble(expiration));
                    break;
                default:
                    cookie.Expires = DateTime.Now.AddHours(Convert.ToDouble(expiration));
                    break;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookieValue(string key)
        {
            if ((HttpContext.Current.Request.Cookies[key] != null))
            {
                return HttpContext.Current.Request.Cookies[key].Value;
            }
            return string.Empty;
        }

        public static void ClearCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Values.Clear();
                System.Web.HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

    }
}
