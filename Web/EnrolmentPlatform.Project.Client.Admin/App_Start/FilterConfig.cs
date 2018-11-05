using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Filter;

namespace EnrolmentPlatform.Project.Client.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter());
            filters.Add(new AjaxExceptionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
