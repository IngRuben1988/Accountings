using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VTAworldpass
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            "downloadattachment",                                              // Route name
            "{controller}/{action}/{id}/{type}",                           // URL with parameters
            new { controller = "utilsapp", action = "downloadbinarydocumentBills", id = UrlParameter.Optional, type = UrlParameter.Optional }  // Parameter defaults
        );
        }
    }
}
