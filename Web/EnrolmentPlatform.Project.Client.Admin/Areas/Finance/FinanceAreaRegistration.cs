using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Finance
{
    public class FinanceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Finance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Finance_default",
                "Finance/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "EnrolmentPlatform.Project.Client.Admin.Areas.Finance.Controllers" }
            );
        }
    }
}