using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Finance.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Finance/Invoice
        /// <summary>
        /// 发票
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 发票申请
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }
    }
}