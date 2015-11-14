using System.Web.Mvc;
using WebFromScratch.Constants.HomeController;

namespace WebFromScratch.Controllers
{
    public class HomeController : Controller
    {
        [Route("", Name = HomeControllerRoute.GetIndex)]
        public ActionResult Index()
        {
            return View(HomeControllerView.Index);
        }

        [Route("about", Name = HomeControllerRoute.GetAbout)]
        public ActionResult About()
        {
            return View(HomeControllerView.About);
        }
    }
}