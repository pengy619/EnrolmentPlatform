using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Castle
{
    public interface ICachingProvider
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);
    }
    /// <summary>
    /// 为了后面注册类型而专门定义的。
    /// </summary>
    public interface IInterceptorLogic
    {
    }
    /// <summary>
    /// 定义这个接口是针对在方法中使用了自定义类的时候，识别出这个类对应的缓存键。
    /// </summary>
    public interface IQCachable
    {
        string CacheKey { get; }
    }
}
