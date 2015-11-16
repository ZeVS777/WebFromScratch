using System.Web.Mvc;
using System.Web.Routing;

namespace WebFromScratch
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Нормализация виртуальных путей для оптимизации SEO
            routes.AppendTrailingSlash = true;
            routes.LowercaseUrls = true;

            // Игнорирование путей
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Ignore("css/{*pathInfo}");
            routes.Ignore("js/{*pathInfo}");
            routes.Ignore("img/{*pathInfo}");
            routes.Ignore("error/forbidden.html");
            routes.Ignore("error/gatewaytimeout.html");
            routes.Ignore("error/serviceunavailable.html");
            routes.Ignore("humans.txt");

            // Включение маршрутов по аттрибутам
            routes.MapMvcAttributeRoutes();

            // Данный маршрут нужен только для системы логирования Elmah.MVC.
            // У этой системы есть баг, из-за которого некоторые 404 и 500 ошибки не логируются без 
            // регистрации такого маршрута.
            // https://github.com/alexbeletsky/elmah-mvc/issues/60
            // https://github.com/RehanSaeed/ASP.NET-MVC-Boilerplate/issues/8
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}