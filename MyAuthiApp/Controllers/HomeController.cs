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
        
       public ActionResult Logout()
        {

            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");

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

               public ActionResult Login(string UserName, string Password, string returnUrl)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(UserName, Password);

            if (user != null)
            {
                var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                Session["User"] = user;
                

               return RedirectToLocal(returnUrl);
                return RedirectToAction("Index", "Home");
            }

                       ViewBag.error = "نام کاربری یا گذرواژه نامعتبر";
            return View();
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
