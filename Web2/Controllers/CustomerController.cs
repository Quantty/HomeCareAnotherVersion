using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class CustomerController : Controller
    {
        private DbWrapper DBLink = new DbWrapper();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public List<ApplicationUser> getEmployees()
        {
            List<ApplicationUser> employeeUsers = new List<ApplicationUser>();

            foreach (ApplicationUser user in UserManager.Users)
            {
                if (!user.Roles.First().Equals("Employee"))
                {
                    employeeUsers.Add(user);
                }
            }
            return employeeUsers;
        }

        public List<ApplicationUser> getCustomers()
        {
            List<ApplicationUser> customersUsers = new List<ApplicationUser>();

            foreach (ApplicationUser user in UserManager.Users)
            {
                if (!user.Roles.First().Equals("Customer"))
                {
                    customersUsers.Add(user);
                }
            }
            return customersUsers;
        }

        public async Task<ActionResult> Index()
        {
            MasterMixViewModel masterModel = new MasterMixViewModel();
            var schedules = DBLink.GetSchedules();
            var employeeUsers = getEmployees();
            var customerUsers = getCustomers();
            var relatives = DBLink.GetRelatives();

            List<MixViewModel> scheduleList = new List<MixViewModel>();

            foreach (var schedule in schedules)
            {
                MixViewModel mymodel = new MixViewModel();
                var userE = await UserManager.FindByIdAsync(schedule.employee_Id + "");
                var userC = await UserManager.FindByIdAsync(schedule.customer_Id + "");

                mymodel.employee = userE;
                mymodel.schedule = schedule;
                mymodel.customer = userC;
                mymodel.task = DBLink.getTaskById(schedule.task_Id);

                //if the employee's id is equal to the id we selected in UserList
                scheduleList.Add(mymodel);

            }
            masterModel.mixViewModel = scheduleList;
            masterModel.relatives = relatives.ToList();

            return View(masterModel);
        }
    }
}