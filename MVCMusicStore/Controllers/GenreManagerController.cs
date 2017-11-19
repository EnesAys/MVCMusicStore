using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVCMusicStore.Controllers
{
    public class GenreManagerController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _GenreList(int? page)
        {
            var genres = db.Genres.ToList();
            if (genres.Count <= 0)
            {
                ViewBag.State = "empty";
                return PartialView();
            }
            return PartialView(genres.ToPagedList(page ?? 1, 3));
        }

        public ActionResult AddGenre()
        {
            ViewBag.Process = "AddGenre";
            return PartialView("_GenreForm");
        }

        [HttpPost]
        public ActionResult AddGenre(Genre genre, int? page)
        {
            db.Genres.Add(genre);
            db.SaveChanges();
            return PartialView("_GenreList", db.Genres.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult UpdateGenre(int genreId)
        {
            ViewBag.Process = "UpdateGenre";
            return PartialView("_GenreForm", db.Genres.Find(genreId));
        }

        [HttpPost]
        public ActionResult UpdateGenre(Genre genre, int? page)
        {
            Genre updated = db.Genres.Find(genre.Id);
            db.Entry(updated).CurrentValues.SetValues(genre);
            db.SaveChanges();
            return PartialView("_GenreList", db.Genres.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult DeleteGenre(int genreId, int? page)
        {
            db.Genres.Remove(db.Genres.Find(genreId));
            db.SaveChanges();
            var genres = db.Genres.ToList().ToPagedList(page ?? 1, 3);
            if (genres.Count <= 0)
            {
                return PartialView("_GenreList", db.Genres.ToList().ToPagedList(page - 1 ?? 1, 3));
            }
            return PartialView("_GenreList", genres);
        }

    }
}
