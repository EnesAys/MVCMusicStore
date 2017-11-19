using MVCMusicStore.Models;
using MVCMusicStore.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace MVCMusicStore
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_PreRequestHandlerExecute()
        {
            if (Session["lang"] != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["lang"].ToString());
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["lang"].ToString());
            }
        }

        protected void Application_Start()
        {


            MusicStoreEntities db = new MusicStoreEntities();
            var genres = db.Genres.ToList();





            #region Simple Membership Adimlari
            //1.WebMatrix.Data ve WebMatrix.WebData 2. verisyonlarini ekleme(copy local=true)
            //2.Yoksa connection stringi olusturma
            //3.appsettingse simplemembership olusturma icin gerekli degeri ekleme
            //4.Membership provideri duzenleme
            //5.RoleManageri duzenleme(enabled=true)
            //6.Tablolarin veritabaninda olusturulmasi icin kod ile tetikleme islemi 
            #endregion

            if (!WebSecurity.Initialized)
            {
            
                WebSecurity.InitializeDatabaseConnection("MusicStoreCon", "UserDetails", "Id", "UserName", autoCreateTables: true);

                if (!WebSecurity.UserExists(ConfigurationManager.AppSettings["DefaultAdminName"]))
                {
                    WebSecurity.CreateUserAndAccount(ConfigurationManager.AppSettings["DefaultAdminName"], ConfigurationManager.AppSettings["DefaultAdminPassword"], new
                    {
                        FirstName = ConfigurationManager.AppSettings["DefaultAdminName"],
                        Email = "a@a.com",
                        BirthDate = DateTime.Now,
                        Phone = "3534554",
                        IsLocked = false,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now
                    });
                    if (!Roles.RoleExists("admin"))
                        Roles.CreateRole("admin");
                    Roles.AddUserToRole(ConfigurationManager.AppSettings["DefaultAdminName"], "admin");
                }
            }

            AreaRegistration.RegisterAllAreas();
            ViewEngines.Engines.Add(new MyViewEngine());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            Session["cart"] = new MyCart();
            Session["lang"] = "tr-TR";
        }
    }
}