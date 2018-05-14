using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class AdminController : Controller
    {
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

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
<<<<<<< HEAD
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
                Console.WriteLine("is valid");
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Console.WriteLine("succeeded");
                    var roleName = Request["UserRole"];
                    await UserManager.AddToRoleAsync(user.Id, roleName);

                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("sadasdas");
            }

            // If we got this far, something failed, redisplay form
=======
            var user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
>>>>>>> 0f3efe61fdd32a02358b261d412e75f6f4aa14b8
            return RedirectToAction("UserTable");
        }
    }
}