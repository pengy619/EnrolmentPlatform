using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using EnrolmentPlatform.Project.Attributes.API;
namespace EnrolmentPlatform.Project.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // cors配置
            var cors = new EnableCorsAttribute(
                origins: "*",
                headers: "*",
                methods: "*"
            );
            config.EnableCors(cors);
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ApiSecurityFilter());
            config.Filters.Add(new ApiExceptionFilter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
              name: "DefaultAreaApi",
              routeTemplate: "api/{area}/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );
        }
    }
}
