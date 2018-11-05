using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Setting.Controllers
{
    public class PluginForPayController : BaseController
    {
        // GET: Setting/PluginForPay
        /// <summary>
        /// 支付插件
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}