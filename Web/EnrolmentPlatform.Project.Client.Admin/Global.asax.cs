using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EnrolmentPlatform.Project.Infrastructure;
using log4net.Config;

namespace EnrolmentPlatform.Project.Client.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Configs/Log4net.config")));
            //WebApiHelper api = new WebApiHelper();
            //api.HttpClientPreheat();
        }
    }
}
