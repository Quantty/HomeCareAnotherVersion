using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
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

        // GET: Admin

       
        public  ActionResult Index()
        {
            var Users = UserManager.Users;
            //Users.First().Roles.First().ToString();
            ViewBag.Title =  UserManager.GetRolesAsync(Users.First().Id).Result.First();
            return View();
        }
        public ActionResult UserTable()
        {
            var Users = UserManager.Users;
            
            return View(Users);
        }


        public ActionResult Delete()
        {
            var Users = UserManager.Users;
            ViewBag.Title = Users.First().UserName;
            return View();
        }
        public ActionResult TaskList()
        {
            var tasks = DBLink.GetTasks();

            return View(tasks);
        }


        [HttpPost, ActionName("CreateTask")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTask(CustomerTask task)
        {
            DBLink.addTask(task);
            return RedirectToAction("TaskList");
        }
        [HttpPost, ActionName("EditTask")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask(CustomerTask task)
        {
            DBLink.updateTask(task);
            return RedirectToAction("TaskList");
        }

        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTask(CustomerTask task)
        {
            DBLink.deleteTaskById(task.Id);
            return RedirectToAction("TaskList");
        }
       
      
        public ActionResult EditTask(int id)
        {

            return View(DBLink.getTaskById(id));
        }

        public ActionResult DeleteTask(int id)
        {

            return View(DBLink.getTaskById(id));
        }

        public ActionResult DetailsTask(int id)
        {
            return View(DBLink.getTaskById(id));
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            return View();
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

        public async Task<ActionResult> ScheduleList()
        {
            var schedule = DBLink.GetSchedules();
            var employeeUsers = getEmployees();
            var customerUsers = getCustomers();

            List<MixViewModel> scheduleList = new List<MixViewModel>();

            foreach (var sched in schedule)
            {
                MixViewModel mymodel = new MixViewModel();
                var userE = await UserManager.FindByIdAsync(sched.employee_Id +"");
                var userC = await UserManager.FindByIdAsync(sched.customer_Id + "");

                mymodel.employee = userE;
                mymodel.schedule = sched;
                mymodel.customer = userC;
                mymodel.task = DBLink.getTaskById(sched.task_Id);

                scheduleList.Add(mymodel);

            }
            return View(scheduleList);
        }


        [HttpPost, ActionName("CreateSchedule")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSchedule(Schedule schedule)
        {
            return RedirectToAction("TimeCheck",schedule);
        }


        [HttpGet]
        public ActionResult TimeCheck(Schedule schedule)
        {
            ViewBag.tasks = DBLink.GetTasks().ToList();
            ViewBag.customers = getCustomers();
            ViewBag.employees = getEmployees();
            string hours = "";
            foreach(var item in DBLink.GetSchedules())
            {
                if (item.employee_Id == schedule.employee_Id && item.date == schedule.date)
                {
                    hours = hours +item.time+"-"+(item.time+DBLink.getTaskById(item.task_Id).duration)+", ";
                }
            }
            if(hours == "")
            {
                ViewBag.busyTime = "All Free";
            }
            else
            {
                ViewBag.busyTime = hours;
            }

            return View(schedule);
        }

        [HttpPost, ActionName("TimeCheck")]
        [ValidateAntiForgeryToken]
        public ActionResult TimeConfirmation(Schedule schedule)
        {
            DBLink.addSchedule(schedule);
            return RedirectToAction("ScheduleList");
        }




        [HttpPost, ActionName("EditSchedule")]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchedule(Schedule schedule)
        {
            DBLink.updateSchedule(schedule);
            return RedirectToAction("ScheduleList");
        }

        [HttpPost, ActionName("DeleteSchedule")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSchedule(Schedule schedule)
        {
            DBLink.deleteScheduleById(schedule.Id);
            return RedirectToAction("ScheduleList");
        }


        public ActionResult EditSchedule(int id)
        {
            ViewBag.customers = getCustomers();
            ViewBag.employees = getEmployees();
            ViewBag.tasks = DBLink.GetTasks();
            return View(DBLink.getScheduleById(id));
        }

        public ActionResult DeleteSchedule(int id)
        {
            var schedule = DBLink.getScheduleById(id);
            return View(schedule);
        }

        public ActionResult DetailsSchedule(int id)
        {
            var schedule = DBLink.getScheduleById(id);
           
            return View(schedule);
        }

        [HttpGet]
        public ActionResult CreateSchedule()
        {
            ViewBag.tasks = DBLink.GetTasks().ToList();
            ViewBag.customers = getCustomers();
            ViewBag.employees = getEmployees();

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
            return RedirectToAction("UserTable");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleName = Request["UserRole"];
                    await UserManager.AddToRoleAsync(user.Id, roleName);

                    ViewBag.Title = "user " + model.Email + " successfully created";
                    return RedirectToAction("UserTable");
                }
                ViewBag.Title = "result didn't succeed";
                return RedirectToAction("UserTable");
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("UserTable");
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.title = "user not found";
                return RedirectToAction("UserTable");
            }
            return View(user);
        }
        
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUser model)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.title = "user not found";
                return RedirectToAction("UserTable");
            }
            var newPassword = Request["Password"];
            if (newPassword.Length > 0)
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(newPassword);
            var roleName = Request["UserRole"];
            if (roleName.Length > 0) {
                var roles = await UserManager.GetRolesAsync(user.Id);
                await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray());
                await UserManager.AddToRoleAsync(user.Id, roleName);
            }
            user.UserName = model.Email;
            user.Email = model.Email;

            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                //throw exception......
            }
            return RedirectToAction("UserTable");
        }

        public async Task<ActionResult> EmployeeSchedules(string id)
        {
            var schedules = DBLink.GetSchedules();
            var employeeUsers = getEmployees();
            var customerUsers = getCustomers();

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

                if (userE != null && userE.Id == id) {//if the employee's id is equal to the id we selected in UserList
                    scheduleList.Add(mymodel);
                }

            }
            return View(scheduleList);
        }




        ///////
    }
}