using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

using System.Web.Http;
using Autofac.Extras.DynamicProxy;
using Autofac;
using Autofac.Integration.WebApi;
namespace EnrolmentPlatform.Project.WebApi.WebLibrary
{
    public class AutofacContainerBuilder
    {
        public void BuildContainer()
        {
            var builder = new ContainerBuilder();
            #region DAL程序集注入
            var dalAssembly = Assembly.Load("EnrolmentPlatform.Project.DAL");
            builder.RegisterAssemblyTypes(dalAssembly).AsImplementedInterfaces();
            #endregion

            #region BLL程序集注入
            var bllAssembly = Assembly.Load("EnrolmentPlatform.Project.BLL");
            builder.RegisterAssemblyTypes(bllAssembly).AsImplementedInterfaces();
            #endregion

            #region WebApi注入
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            #endregion

            #region 缓存AOP注入
            var assembly = this.GetType().GetTypeInfo().Assembly;
            builder.RegisterType<CachingProvider>().As<ICachingProvider>();
            builder.RegisterType<QCachingInterceptor>();

            builder.RegisterAssemblyTypes(bllAssembly)
                .Where(type => typeof(IInterceptorLogic).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(QCachingInterceptor));

            #endregion

            var container = builder.Build();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DIContainer.RegisterContainer(container);
        }
    }
}