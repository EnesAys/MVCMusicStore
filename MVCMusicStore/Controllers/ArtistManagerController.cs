using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVCMusicStore.Controllers
{
    public class ArtistManagerController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ArtistList(int? page)
        {
            var artists = db.Artists.ToList();
            if (artists.Count <= 0)
            {
                ViewBag.State = "empty";
                return PartialView();
            }
            return PartialView(artists.ToPagedList(page ?? 1, 5));
        }

        public ActionResult AddArtist()
        {
            ViewBag.Process = "AddArtist";
            return PartialView("_ArtistForm");
        }

        [HttpPost]
        public ActionResult AddArtist(Artist artist, int? page)
        {
            db.Artists.Add(artist);
            db.SaveChanges();
            return PartialView("_ArtistList", db.Artists.ToList().ToPagedList(page ?? 1, 5));
        }

        public ActionResult UpdateArtist(int artistId)
        {
            ViewBag.Process = "UpdateArtist";
            return PartialView("_ArtistForm", db.Artists.Find(artistId));
        }

        [HttpPost]
        public ActionResult UpdateArtist(Artist artist, int? page)
        {
            Artist updated = db.Artists.Find(artist.Id);
            db.Entry(updated).CurrentValues.SetValues(artist);
            db.SaveChanges();
            return PartialView("_ArtistList", db.Artists.ToList().ToPagedList(page ?? 1, 5));
        }

        public ActionResult DeleteArtist(int artistId, int? page)
        {
            db.Artists.Remove(db.Artists.Find(artistId));
            db.SaveChanges();
            var artists = db.Artists.ToList().ToPagedList(page ?? 1, 5);
            if (artists.Count <= 0)
            {
                return PartialView("_ArtistList", db.Artists.ToList().ToPagedList(page - 1 ?? 1, 5));
            }
            return PartialView("_ArtistList", artists);
        }

    }
}
