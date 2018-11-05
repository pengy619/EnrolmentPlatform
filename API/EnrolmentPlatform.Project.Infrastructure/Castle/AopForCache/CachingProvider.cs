using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Castle
{

    public class CachingProvider : ICachingProvider
    {
        public object Get(string cacheKey)
        {
            return CacheHelper.GetCache(cacheKey); //先用本地缓存，如果用Redis缓存Dto需要加Serializable,Dto加了Serializable 对webapi传值有影响
        }

        public void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            CacheHelper.SetCache(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
        }
    }
}
