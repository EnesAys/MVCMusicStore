using MVCMusicStore.Models;
using MVCMusicStore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace MVCMusicStore.Controllers
{
   
    public class AccountController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDetail user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!Roles.RoleExists("user"))
                        Roles.CreateRole("user");
                    string confirmationToken = WebSecurity.CreateUserAndAccount(user.UserName, user.Password, new { user.Address, user.Email, user.FirstName, user.LastName, IsDeleted = false, IsLocked = false, CreatedDate = DateTime.Now, user.BirthDate, user.Phone }, true);
                    Roles.AddUserToRole(user.UserName, "user");
                    MailHelper.SendConfirmationMail(user.UserName, user.Password, user.Email, confirmationToken);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDetail user, string ReturnUrl)
        {
            UserDetail userDetail = db.UserDetails.FirstOrDefault(ud => ud.UserName == user.UserName);
            if (userDetail != null)
            {
                if (userDetail.IsDeleted || userDetail.IsLocked)
                {
                    if (userDetail.IsLocked)
                        ViewBag.Error = "You're banned!";
                    else if (userDetail.IsDeleted)
                        ViewBag.Error = "This user doesn't exist anymore.";
                    return View(user);
                }
                else
                {
                    if (WebSecurity.Login(user.UserName, user.Password))
                    {
                        if (ReturnUrl != null)
                        {
                            string[] paths = ReturnUrl.Split('/');
                            if (paths.Length > 2)
                                return RedirectToAction(paths[2], paths[1]);
                            else
                                return RedirectToAction("Index", paths[1]);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = "Invalid username or password";
                        return View(user);
                    }
                }
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View(user);
            }

        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile()
        {
            var currentUser = db.UserDetails.Find(WebSecurity.CurrentUserId);
            return View(currentUser);
        }

        public ActionResult Activation(string confirmationToken)
        {
            ViewBag.IsConfirmed = false;
            if (WebSecurity.ConfirmAccount(confirmationToken))
                ViewBag.IsConfirmed = true;
            return View();
        }
    }
}
