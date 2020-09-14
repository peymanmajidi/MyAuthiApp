using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MyAuthiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAuthiApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Register(string username)
        {
            var pass = "abcABC@123";
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            
            manager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequiredLength = 4,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false

            };

            var user = new IdentityUser() { UserName = username };
            var result = manager.Create(user, pass);
            var id = user.Id;
            //var db = new BookDataContext();
            //var new_user = db.AspNetUsers.FirstOrDefault(u
            //    => u.Id.Equals(id));

            //new_user.Age = 32;
            //db.SaveChanges();

            return Content($"{username} added!" + result.Errors.FirstOrDefault());
        }

        [HttpGet]

        public ActionResult Login(string username, string password)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(username, password);

            if (user != null)
            {
                // should install
                // Microsoft.AspNet.Identity.EntityFramework
                // Microsoft.AspNet.Identity.Owin
                // add Startup.cs
                // then
                // Microsoft.Owin.Host.SystemWeb
                var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                return Content("Boom!");
            }
            else
                return Content("Invalid username or password.");


          
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
