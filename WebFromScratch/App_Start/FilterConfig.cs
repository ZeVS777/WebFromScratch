using System.Web.Mvc;
using System.Web.Routing;
using GlobalMvcHelpers.Filters;

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
        }
    }
}