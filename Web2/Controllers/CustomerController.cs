using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var schedules = cont.GetSchedules();

            dynamic mymodel = new ExpandoObject();
            mymodel.tasks = tasks;
            mymodel.schedules = schedules;
            return View(mymodel);
        }
    }
}