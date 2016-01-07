using System.Web.Mvc;
using System.Web.Routing;
using GlobalMvcHelpers.Filters;
using NWebsec.Mvc.HttpHeaders;

namespace WebFromScratch
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // SEO
            filters.Add(new RedirectToCanonicalUrlAttribute(
                RouteTable.Routes.AppendTrailingSlash,
                RouteTable.Routes.LowercaseUrls));

            // HTTPS only
            filters.Add(new RedirectToHttpsAttribute(true));

            // Убрать кеширование страницы.
            // Cache-Control: no-cache, no-store, must-revalidate Expires: -1 Pragma: no-cache
            // Возможно следует запретить кеширование только на специфичных сраницах,
            // таких как платёжная система
            filters.Add(new SetNoCacheHttpHeadersAttribute());
        }
    }
}