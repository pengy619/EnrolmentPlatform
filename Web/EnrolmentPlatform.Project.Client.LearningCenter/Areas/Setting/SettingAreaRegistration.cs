using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Setting
{
    public class SettingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Setting";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Setting_default",
                "Setting/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "EnrolmentPlatform.Project.Client.LearningCenter.Areas.Setting.Controllers" }
            );
        }
    }
}