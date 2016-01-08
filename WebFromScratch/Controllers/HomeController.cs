﻿using System;
using System.Net;
using System.Text;
using System.Web.Mvc;
using GlobalMvcHelpers.Filters;
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


        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
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
        [Route("manifest.json", Name = HomeControllerRoute.GetManifestJson)]
        public ActionResult GetManifestJson()
        {
            string content = this._manifestService.GetManifestJson();
            return this.Content(content, ContentType.Json, Encoding.UTF8);
        }
    }
}