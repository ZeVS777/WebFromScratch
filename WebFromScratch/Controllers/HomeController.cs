using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web.Mvc;
using GlobalMvcHelpers;
using GlobalMvcHelpers.Filters;
using NWebsec.Mvc.HttpHeaders;
using WebFromScratch.Resources.Constants;
using WebFromScratch.Services;

namespace WebFromScratch.Controllers
{
    public class HomeController : Controller
    {

        private readonly IManifestService _manifestService;
        private readonly IBrowserConfigService _browserConfigService;

        public HomeController(
            IManifestService manifestService,
            IBrowserConfigService browserConfigService)
        {
            _manifestService = manifestService;
            _browserConfigService = browserConfigService;
        }

        [SetNoCacheHttpHeaders]
        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
            Trace.WriteLine(string.Format(
                "Index page requested. User Agent:<{0}>.",
                Request.Headers.Get("User-Agent")));
            return View(HomeControllerView.Index);
        }

        [Route("home/error/{status}")]
        public ActionResult Error(int status)
        {
            switch (status)
            {
                case 403:
                    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return Content("Oops 403");
                case 404:
                    return HttpNotFound("Oops 404!");
                case 500:
                default:
                    throw new Exception("Oops 500!");

            }
        }

        [Route("about", Name = HomeControllerRoute.GetAbout)]
        public ActionResult About()
        {
            return View(HomeControllerView.About);
        }

        [NotACanonicalUrl]
        //[OutputCache(CacheProfile = HomeControllerCacheProfile.ManifestJson)]
        [Route("manifest.json", Name = HomeControllerRoute.GetManifestJson)]
        public ContentResult GetManifestJson()
        {
            Trace.WriteLine(string.Format(
                "manifest.json requested. User Agent:<{0}>.",
                Request.Headers.Get("User-Agent")));
            string content = _manifestService.GetManifestJson();
            return Content(content, ContentType.Json, Encoding.UTF8);
        }

        [NotACanonicalUrl]
        //[OutputCache(CacheProfile = HomeControllerCacheProfile.BrowserConfigXml)]
        [Route("browserconfig.xml", Name = HomeControllerRoute.GetBrowserConfigXml)]
        public ContentResult BrowserConfigXml()
        {
            Trace.WriteLine(string.Format(
                "browserconfig.xml requested. User Agent:<{0}>.",
                Request.Headers.Get("User-Agent")));
            string content = _browserConfigService.GetBrowserConfigXml();
            return Content(content, ContentType.Xml, Encoding.UTF8);
        }
    }
}