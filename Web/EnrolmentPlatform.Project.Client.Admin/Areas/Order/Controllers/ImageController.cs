using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Order.Controllers
{
    public class ImageController : BaseController
    {
        // GET: Order/Image
        public ActionResult Index()
        {
            return View();
        }
    }
}