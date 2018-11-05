using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Sale
{
    public class SaleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sale";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sale_default",
                "Sale/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Sale.Controllers" }
            );
        }
    }
}