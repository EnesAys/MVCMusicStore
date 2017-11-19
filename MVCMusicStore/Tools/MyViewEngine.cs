using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStore.Tools
{
    public class MyViewEngine : RazorViewEngine
    {
        public MyViewEngine()
            : base()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Concat(new[]{
                "~/Views/Shared/Partials/{0}.cshtml"
            }).ToArray();
        }
    }
}