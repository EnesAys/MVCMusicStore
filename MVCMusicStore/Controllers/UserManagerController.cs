using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Validation;

namespace MVCMusicStore.Controllers
{
    public class UserManagerController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _UserList(int? page)
        {
            var users = db.UserDetails.Where(ud => ud.IsDeleted == false).ToList();
            if (users.Count <= 0)
            {
                ViewBag.State = "empty";
                return PartialView();
            }
            return PartialView(users.ToPagedList(page ?? 1, 10));
        }

        [HttpPost]
        public ActionResult DeleteUser(int userId, int? page)
        {
            UserDetail user = db.UserDetails.Find(userId);
            user.IsDeleted = true;
            user.Password = user.ConfirmPassword = "asdf";
            db.SaveChanges();


            var users = db.UserDetails.Where(ud => ud.IsDeleted == false).ToList().ToPagedList(page ?? 1, 10);
            if (users.Count <= 0)
            {
                return PartialView("_UserList", db.UserDetails.ToList().Where(ud => ud.IsDeleted == false).ToPagedList(page - 1 ?? 1, 10));
            }
            return PartialView("_UserList", users);
        }

        [HttpPost]
        public ActionResult BanUser(int userId, int? page)
        {
            UserDetail user = db.UserDetails.Find(userId);
            user.IsLocked = true;
            user.Password = user.ConfirmPassword = "asdf";
            db.SaveChanges();
            return PartialView("_UserList", db.UserDetails.Where(ud=>ud.IsDeleted==false).ToList().ToPagedList(page ?? 1, 10));
        }

        [HttpPost]
        public ActionResult ActivateUser(int userId, int? page)
        {
            UserDetail user = db.UserDetails.Find(userId);
            user.IsLocked = false;
            user.Password = user.ConfirmPassword = "asdf";
            db.SaveChanges();
            return PartialView("_UserList", db.UserDetails.Where(ud => ud.IsDeleted == false).ToList().ToPagedList(page ?? 1, 10));
        }
    }
}
