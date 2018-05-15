using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web2.Controllers
{
    public class CustomerController : Controller
    {
 
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskList()
        {
            var cont = new DbWrapper();
            
            //var Tasks = othermanager.Tasks;
            var tasks = cont.GetTasks();
            return View(tasks);
        }
    }
}