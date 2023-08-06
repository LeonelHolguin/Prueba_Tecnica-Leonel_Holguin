using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RouletteGame.Server
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Api",
                "api/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional }
                );

            routes.MapRoute(
                "ApiUpdate",
                "api/Player/Update/{id}",
                new { id = UrlParameter.Optional }
                );
        }
    }
}
