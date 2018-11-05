using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.FileStorage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
