using MVCMusicStore.Models;
using MVCMusicStore.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MVCMusicStore.Controllers
{
    public class CartController : Controller
    {

        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _CartButton()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddToCart(int albumId)
        {
            MyCart myCart = Session["cart"] as MyCart;
            Album added = db.Albums.Find(albumId);
            myCart.AddToCart(new CartItem
            {
                Count = 1,
                Id = albumId,
                Name = added.Title,
                Price = added.Price
            });
            return PartialView("_CartButton");
        }

        public ActionResult _CartItems()
        {
            MyCart myCart = Session["cart"] as MyCart;
            return PartialView(myCart.CartItems);
        }

        public ActionResult RemoveFromCart(int albumId)
        {
            MyCart myCart = Session["cart"] as MyCart;
            myCart.RemoveFromcart(albumId);
            Session["cart"] = myCart;
            return Json(new
            {
                CartButton = RenderRazorViewToString("_CartButton", null),
                CartTable = RenderRazorViewToString("_CartItems", myCart.CartItems).ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Checkout()
        {
            MyCart myCart = Session["cart"] as MyCart;
            UserDetail currentUser = db.UserDetails.Find(WebSecurity.CurrentUserId);
            Tuple<MyCart, UserDetail> t = new Tuple<MyCart, UserDetail>(myCart, currentUser);
            return View(t);
        }

        [HttpPost, ActionName("Checkout")]
        public ActionResult ConfirmCheckout()
        {
            MyCart myCart = Session["cart"] as MyCart;
            UserDetail currentUser = db.UserDetails.Find(WebSecurity.CurrentUserId);
            Order newOrder = new Order()
            {
                Email = currentUser.Email,
                Phone = currentUser.Phone,
                ShipAddress = currentUser.Address,
                UserDetailId = currentUser.Id
            };
            db.Orders.Add(newOrder);
            foreach (var item in myCart.CartItems)
            {
                OrderDetail od = new OrderDetail()
                {
                    Count = item.Count,
                    AlbumId = item.Id,
                    Discount = 0,
                    Order = newOrder,
                    Price = item.Price
                };
                db.OrderDetails.Add(od);
            }
            db.SaveChanges();
            myCart.ClearCart();
            return RedirectToAction("Index", "Home");
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
