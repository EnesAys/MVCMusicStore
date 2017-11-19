using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MVCMusicStore.Models;
using MVCMusicStore.Tools;
using System.Web.Script.Serialization;

namespace MVCMusicStore.Controllers
{
    [Authorize (Roles ="Admin")]
    public class AlbumManagerController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _AlbumList(int? page)
        {
            var albums = db.Albums.ToList();
            if (albums.Count <= 0)
            {
                ViewBag.State = "empty";
                return PartialView();
            }
            return PartialView(albums.ToPagedList(page ?? 1, 10));
        }

        public ActionResult AddAlbum()
        {
            ViewBag.Artists = db.Artists.ToList();
            ViewBag.Genres = db.Genres.ToList();
            ViewBag.Process = "AddAlbum";
            return PartialView("_AlbumForm");
        }

        [HttpPost]
        public ActionResult AddAlbum(Album album, int? page)
        {
            ViewBag.Artists = db.Artists.ToList();
            ViewBag.Genres = db.Genres.ToList();
            db.Albums.Add(album);
            db.SaveChanges();
            return PartialView("_AlbumList", db.Albums.ToList().ToPagedList(page ?? 1, 10));
        }

        public ActionResult UpdateAlbum(int albumId)
        {
            ViewBag.Artists = db.Artists.ToList();
            ViewBag.Genres = db.Genres.ToList();
            ViewBag.Process = "UpdateAlbum";
            return PartialView("_AlbumForm", db.Albums.Find(albumId));
        }

        [HttpPost]
        public ActionResult UpdateAlbum(Album album, int? page)
        {
            ViewBag.Artists = db.Artists.ToList();
            ViewBag.Genres = db.Genres.ToList();
            if (!ModelState.IsValid)
                return PartialView("_AlbumForm", album);
            Album updated = db.Albums.Find(album.Id);
            db.Entry(updated).CurrentValues.SetValues(album);
            db.SaveChanges();
            return PartialView("_AlbumList", db.Albums.ToList().ToPagedList(page ?? 1, 10));
        }

        public ActionResult DeleteAlbum(int albumId, int? page)
        {
            Album album = db.Albums.Find(albumId);
            if (album.AlbumArtUrl != "/Content/Images/placeholder.gif")
                System.IO.File.Delete(Server.MapPath(album.AlbumArtUrl));
            db.Albums.Remove(album);
            db.SaveChanges();
            var albums = db.Albums.ToList().ToPagedList(page ?? 1, 10);
            if (albums.Count <= 0)
            {
                return PartialView("_AlbumList", db.Albums.ToList().ToPagedList(page - 1 ?? 1, 10));
            }
            return PartialView("_AlbumList", albums);
        }

        public string AddPicture(int id, string data)
        {
            PictureToSave pictureToSave = new JavaScriptSerializer().Deserialize(data, typeof(PictureToSave)) as PictureToSave;
            string imagePath = "/Content/AlbumImages/" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5) + ".jpg";
            pictureToSave.SmallPicture.Save(Server.MapPath(imagePath));
            if (id > 0)
            {
                Album album = db.Albums.Find(id);
                if (album.AlbumArtUrl != "/Content/Images/placeholder.gif")
                    System.IO.File.Delete(Server.MapPath(album.AlbumArtUrl));
            }
            return imagePath;
        }
    }
}
