using System.Net;
using System.Web.Mvc;

namespace WebFromScratch.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        ///     Получить представление, полное или частичное,
        /// в зависимости от того, пришёл ли запрос от Ajax или простой.
        /// </summary>
        /// <param name="status">Статус код ошибки, полное или частичное,
        ///  в зависимости от того, пришёл ли запрос от Ajax или простой.</param>
        /// <param name="viewName">Путь до представления</param>
        /// <returns>Представление со страницей ошибки</returns>
        private ActionResult GetErrorView(HttpStatusCode status, string viewName)
        {
            Response.StatusCode = (int) status;

            // Не показывать встроенные страницы ошибок IIS
            Response.TrySkipIisCustomErrors = true;

            ActionResult result;
            if (Request.IsAjaxRequest())
            {
                result = PartialView(viewName);
            }
            else
            {
                result = View(viewName);
            }

            return result;
        }
    }
}