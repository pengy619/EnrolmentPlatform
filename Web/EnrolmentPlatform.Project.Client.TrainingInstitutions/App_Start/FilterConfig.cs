using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Filter;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions
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
