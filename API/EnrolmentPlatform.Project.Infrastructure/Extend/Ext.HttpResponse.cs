using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace EnrolmentPlatform.Project.Infrastructure
{
    public static partial class Ext
    {
        public static HttpResponseMessage ResponseMessage(this Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = obj.ToJson();
            }
            HttpResponseMessage response = new HttpResponseMessage
            {
                Content = new StringContent(
                    str,
                    Encoding.GetEncoding("UTF-8"),
                    "application/json")
            };
            return response;
        }
    }
}
