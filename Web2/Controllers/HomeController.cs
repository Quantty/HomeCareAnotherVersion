using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize (Roles = "Admin")]
        public ActionResult Contact()
        {
            string name = User.Identity.Name;
            ViewBag.Message = name;

            return View();
        }
    }
}