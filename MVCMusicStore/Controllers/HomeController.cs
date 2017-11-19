using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Globalization;

namespace MVCMusicStore.Controllers
{
    public class HomeController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();

        public ActionResult Index(string lang )
        {
            return View();
        }

        public ActionResult _GenrePartial()
        {
            return PartialView(db.Genres.ToList());
        }

        public ActionResult _AlbumScrollDownList(int? page)
        {
         
            return PartialView(db.Albums.ToList().ToPagedList(page ?? 1, 80));
        }

        public ActionResult SetLanguage(string language)
        {
            Session["lang"] = language;
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
