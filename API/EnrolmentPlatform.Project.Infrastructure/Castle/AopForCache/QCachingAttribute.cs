using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Castle
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class QCachingAttribute : Attribute
    {
        /// <summary>
        /// 缓存过期时间 单位秒
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 30;

        //add other settings ...
    }
}
