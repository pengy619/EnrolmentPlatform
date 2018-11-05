using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Content
{
    public class ContentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Content";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Content_default",
                "Content/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}