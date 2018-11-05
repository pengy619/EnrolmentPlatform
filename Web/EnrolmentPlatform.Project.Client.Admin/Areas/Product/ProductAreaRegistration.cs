using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product
{
    public class ProductAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Product";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Product_default",
                "Product/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers" }
            );
        }
    }
}