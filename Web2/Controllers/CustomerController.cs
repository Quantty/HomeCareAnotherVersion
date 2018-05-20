using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class CustomerController : Controller
    {
        DbWrapper DbLink = new DbWrapper();
        // GET: Customer
        public ActionResult Index()
        {

            var sched = DbLink.GetSchedules();
            List<Schedule> schedule = new List<Schedule>();


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