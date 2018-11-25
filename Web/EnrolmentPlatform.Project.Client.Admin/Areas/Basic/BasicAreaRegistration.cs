using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Basic
{
    public class BasicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Basic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Basic_default",
                "Basic/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "EnrolmentPlatform.Project.Client.Admin.Areas.Basic.Controllers" }
            );
        }
    }
}