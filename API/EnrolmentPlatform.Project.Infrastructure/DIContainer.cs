using Autofac;

namespace EnrolmentPlatform.Project.Infrastructure
{
    /// <summary>
    /// 依赖注入容器
    /// </summary>
    /// <remarks>
    /// 对Autofac进行封装
    /// </remarks>
    public class DIContainer
    {
        private static IContainer _container;

        /// <summary>
        /// 注册DIContainer
        /// </summary>
        /// <param name="container">Autofac.IContainer</param>
        public static void RegisterContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// 按类型获取组件
        /// </summary>
        /// <typeparam name="TService">组件类型</typeparam>
        /// <returns>返回获取的组件</returns>
        public static TService Resolve<TService>()
        {
            string className = typeof(TService).Name;
            return _container.Resolve<TService>();
        }

        /// <summary>
        /// 按名称获取组件
        /// </summary>
        /// <typeparam name="TService">组件类型</typeparam>
        /// <param name="serviceName">组件名称</param>
        /// <returns>返回获取的组件</returns>
        public static TService ResolveNamed<TService>(string serviceName)
        {
            return _container.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 按参数获取组件
        /// </summary>
        /// <typeparam name="TService">组件类型</typeparam>
        /// <param name="parameters"><see cref="Autofac.Core.Parameter"/></param>
        /// <returns>返回获取的组件</returns>
        public static TService Resolve<TService>(params Autofac.Core.Parameter[] parameters)
        {
            return _container.Resolve<TService>(parameters);
        }

        /// <summary>
        /// 按key获取组件
        /// </summary>
        /// <typeparam name="TService">组件类型</typeparam>
        /// <param name="serviceKey">枚举类型的Key</param>
        /// <returns>返回获取的组件</returns>
        public static TService ResolveKeyed<TService>(object serviceKey)
        {
            return _container.ResolveKeyed<TService>(serviceKey);
        }

        /// <summary>
        /// 获取InstancePerHttpRequest的组件
        /// </summary>
        /// <typeparam name="TService">组件类型</typeparam>
    }
}
