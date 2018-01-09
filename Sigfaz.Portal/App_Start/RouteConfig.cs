using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sigfaz.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Estados",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Estado", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Cidades",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cidade", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
