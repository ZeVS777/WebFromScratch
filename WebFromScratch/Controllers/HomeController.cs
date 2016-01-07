using System;
using System.Net;
using System.Web.Mvc;
using WebFromScratch.Constants;

namespace WebFromScratch.Controllers
{
    public class HomeController : Controller
    {
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
    }
}