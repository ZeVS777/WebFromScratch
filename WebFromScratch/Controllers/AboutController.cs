using System.Web.Mvc;

namespace WebFromScratch.Controllers
{
    public class AboutController : Controller
    {
        [Route("about")]
        public ActionResult Index()
        {
            return View();
        }
    }
}