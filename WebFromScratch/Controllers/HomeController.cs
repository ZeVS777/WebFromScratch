using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web.Mvc;
using GlobalMvcHelpers.Filters;
using NWebsec.Mvc.HttpHeaders;
using WebFromScratch.Resources.Constants;
using WebFromScratch.Services.ManifestService;
using ContentType = GlobalMvcHelpers.ContentType;

namespace WebFromScratch.Controllers
{
    public class HomeController : Controller
    {

        private readonly IManifestService _manifestService;

        public HomeController(IManifestService manifestService)
        {
            this._manifestService = manifestService;
        }

        [SetNoCacheHttpHeaders]
        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
            Trace.WriteLine(string.Format(
                "Index page requested. User Agent:<{0}>.",
                this.Request.Headers.Get("User-Agent")));
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
        [OutputCache(CacheProfile = CacheProfileName.ManifestJson)]
        [Route("manifest.json", Name = HomeControllerRoute.GetManifestJson)]
        public ActionResult GetManifestJson()
        {
            Trace.WriteLine(string.Format(
                "manifest.json requested. User Agent:<{0}>.",
                this.Request.Headers.Get("User-Agent")));
            string content = this._manifestService.GetManifestJson();
            return this.Content(content, ContentType.Json, Encoding.UTF8);
        }
    }
}