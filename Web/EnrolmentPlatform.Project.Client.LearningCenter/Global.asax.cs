using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net.Config;
using EnrolmentPlatform.Project.Infrastructure;
using Autofac;
using System.Reflection;
using System.Web.Http;

namespace EnrolmentPlatform.Project.Client.LearningCenter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.BuildContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Configs/Log4net.config")));
            WebApiHelper api = new WebApiHelper();
            api.HttpClientPreheat();
        }

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

            var container = builder.Build();
            DIContainer.RegisterContainer(container);
        }
    }
}
