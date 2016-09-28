using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceStation.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "This application has been developed as a test task for Intetics IT-courses 2016";

            return View();
        }
    }
}