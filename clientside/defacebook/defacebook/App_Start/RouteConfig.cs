using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace defacebook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "Home/login",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Loginerror",
                url: "Home/error{id}",
                defaults: new { controller = "Home", action = "error", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "loginsucess",
               url: "Home/sucess{id}",
               defaults: new { controller = "Home", action = "sucess", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "showusers",
              url: "Home/showusers",
              defaults: new { controller = "Home", action = "showusers", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}