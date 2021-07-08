using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHibernateTestProject.Models;
using NHibernateTestProject.Models.ViewModels;
using NHibernateTestProject.NHibernate;

namespace NHibernateTestProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapperSession _session;
        public AccountController(IMapperSession session)
        {
            _session = session;
        }
        [HttpGet]
      
        public IActionResult Login()
        {
          
            return View(new UserLoginViewModel());
        }

        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool userExists = _session.LoginUser(model.username, model.password);
            int userId = _session.GetUserID(model.username, model.password);

            if (userExists)
            {
                var isAdmin = _session.isAdmin(model.username, model.password);
                if (isAdmin)
                {
                    TempData["role"] = "admin";
                    
                    @ViewBag.role = "admin";

                    return RedirectToAction("Index", "Book");
                    //     var User = HttpContext.User;
                    //    var isAdmin = User.IsInRole("Admin");
                }
                else
                {
                    TempData["role"] = "user";
                    TempData["user_id"] = userId;

                    return RedirectToAction("Index", "Book");
                   
            }
          
              
            }
            else
            {
                ModelState.AddModelError("", "Wrong Credentials");

                return View(model);
            }
         
            }
       
        
        [HttpGet]
  
        public IActionResult Register()
        {
       
                return View(new RegisterViewModel());
        }

        [HttpPost]
       [ValidateAntiForgeryToken]

        //Emaili tek register dhe username tek login jane praktikisht e njejta gje
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
          


               var user = new User { username = model.username, email = model.email,role="user", fullname = model.fullname, bankCardNumber = model.bankCardNumber,password=model.password };
                _session.SaveUser(user);
                //send regular user to a controller that will display only the list of books
                    return RedirectToAction("Index", "Book");
              

            }
            return View(model);
        }

        [HttpPost]
       
        public async Task<IActionResult> Logout()
        {
            
            return RedirectToAction("Login", "Account");
        }

    }
}
