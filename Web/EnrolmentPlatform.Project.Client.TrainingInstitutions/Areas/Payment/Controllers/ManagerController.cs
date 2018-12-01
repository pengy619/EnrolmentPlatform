using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Payment.Controllers
{
    public class ManagerController : BaseController
    {
        // GET: Payment/Manager
        public ActionResult Index()
        {
            return View();
        }
    }
}