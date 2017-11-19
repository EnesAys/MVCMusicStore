using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
    public class StoreController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        public ActionResult Browse(int genreId)
        {
            var selectedGenre = db.Genres.Find(genreId);
            if (selectedGenre == null)
                return RedirectToAction("Index");
            return View(selectedGenre);
        }

        public ActionResult Details(int id)
        {
            var selectedAlbum = db.Albums.Find(id);
            if (selectedAlbum == null)
                return RedirectToAction("Index");
            return View(selectedAlbum);
        }


        public ActionResult RateIt(int vote, int artistId)
        {
            Artist artist = db.Artists.Find(artistId);
            artist.Rating = vote;
            db.SaveChanges();
            return Json(new { Vote=vote, Name= artist.Name, Success="You rate it successfully!" }, JsonRequestBehavior.AllowGet);
        }
    }
}
