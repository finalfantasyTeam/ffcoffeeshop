using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ff.coffee.webapp.Models;
using ff.coffee.repository;
using Newtonsoft.Json;

namespace ff.coffee.webapp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            AccountModel accModel = new AccountModel();
            return View(accModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountModel model, string returnUrl)
        {
            if (ModelState.IsValid && model.Login(model.UserName, model.Password, model.RememberMe))
            {
                CoffeePrincipalSerializeModel seriallizeModel = new CoffeePrincipalSerializeModel();

                seriallizeModel.UserId = model.dtoUser.ID;
                seriallizeModel.StaffUserName = model.dtoUser.UserName;
                seriallizeModel.Position = model.dtoUser.Position;
                seriallizeModel.FullName = model.dtoUser.StaffName;
                seriallizeModel.StaffRole = model.dtoUser.Role.Value;
                seriallizeModel.Notes = model.dtoUser.Notes;
                
                string userData = JsonConvert.SerializeObject(seriallizeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1, 
                    model.dtoUser.UserName, 
                    DateTime.Now, 
                    DateTime.Now.AddMinutes(30), 
                    model.RememberMe, 
                    userData);

                string encrypTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie cookieFA = new HttpCookie(FormsAuthentication.FormsCookieName, encrypTicket);
                Response.Cookies.Add(cookieFA);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [CoffeeAuthorize("Admin,Manager")]
        public ActionResult Register()
        {
            RegisterModel regModel = new RegisterModel();
            return View(regModel);
        }

        [HttpPost]
        [CoffeeAuthorize("Admin,Manager")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateAccount();
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    throw new Exception(e.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult UserManagement()
        {
            RegisterModel regVM = new RegisterModel();
            regVM.GetListUser(User.Identity.Name);
            return View(regVM);
        }

        [HttpGet]
        public ActionResult SoftDeleteUser(string userName)
        {
            RegisterModel regVM = new RegisterModel();
            regVM.GetUserByUserName(userName);
            regVM.Enable = false;
            regVM.UpdateUserInfo(regVM.Id);

            return RedirectToAction("UserManagement");
        }

        [HttpGet]
        public ActionResult Manage(string userName = null)
        {
            RegisterModel regVM = new RegisterModel();
            userName = String.IsNullOrEmpty(userName) ? User.Identity.Name : userName;
            regVM.GetUserByUserName(userName);
            return View(regVM);
        }

        [HttpPost]
        public ActionResult Manage(RegisterModel model)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                model.Enable = true;
                model.UpdateUserInfo(model.Id);
            }

            return View(model);
        }
    }
}