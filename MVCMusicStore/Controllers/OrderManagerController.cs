using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Text;

namespace MVCMusicStore.Controllers
{
    public class OrderManagerController : Controller
    {
        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _OrderList(int? page)
        {
            var orders = db.Orders.Where(o => o.IsDeleted == false).ToList();
            if (orders.Count <= 0)
            {
                ViewBag.State = "empty";
                return PartialView();
            }
            return PartialView(orders.ToPagedList(page ?? 1, 10));
        }

        public string GetOrderDetails(int orderId)
        {
            return GetOrderHtml(orderId);
        }

        private string GetOrderHtml(int orderId)
        {
            Order order = db.Orders.Find(orderId);
            if (order == null || order.OrderDetails.Where(od => od.IsDeleted == false).ToList().Count <= 0)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div id='messages'><h3>Details for OrderID:{0}</h3><p>User Name: <b>{1}</b></p><p>Email: <b>{2}</b></p><p>Phone: <b>{3}</b></p><p>Address: <b>{4}</b></p>", order.Id, order.UserDetail.UserName, order.UserDetail.Email, order.UserDetail.Phone, order.UserDetail.Address);
            sb.AppendFormat("<h5>Order Details for OrderID{0}</h5>", order.Id);
            decimal totalPrice = 0;
            foreach (var item in order.OrderDetails)
            {
                totalPrice += item.Count * item.Price * (1 - Convert.ToDecimal(item.Discount));
                sb.AppendFormat("<p><b>{0}</b><br/>Price:<b>{1:C}</b><br/>Count:<b>{2}</b><br/>Discount:<b>{3}</b><br/>SubTotal:<b>{4:C}</b><br/><br/></p>", item.Album.Title, item.Price, item.Count, item.Discount, item.Count * item.Price * (1 - Convert.ToDecimal(item.Discount)));
            }
            sb.AppendFormat("<p style='text-align:right;'>Total Price<b>{0:C}</b></p>", totalPrice);
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}
