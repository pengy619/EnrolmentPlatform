using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Operate
{
    public class OperateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Operate";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Operate_default",
                "Operate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "EnrolmentPlatform.Project.Client.Admin.Areas.Operate.Controllers" }
            );
        }
    }
}