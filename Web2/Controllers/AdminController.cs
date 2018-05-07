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
            var user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
            return RedirectToAction("UserTable");
        }
    }
}