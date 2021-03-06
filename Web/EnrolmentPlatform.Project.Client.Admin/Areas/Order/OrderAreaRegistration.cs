﻿using System.Web.Mvc;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Order
{
    public class OrderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Order";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Order_default",
                "Order/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "EnrolmentPlatform.Project.Client.Admin.Areas.Order.Controllers" }
            );
        }
    }
}