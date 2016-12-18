﻿using System.Web.Mvc;

namespace RoboBank.Customer.Service.Isolated.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger/ui/index");
        }
    }
}
