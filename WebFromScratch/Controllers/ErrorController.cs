using System.Net;
using System.Web.Mvc;
using WebFromScratch.Constants;

namespace WebFromScratch.Controllers
{
    /// <summary>
    ///     Предоставляет методы обработки HTTP запросов с ошибками.
    /// </summary>
    [RoutePrefix("error")]
    public class ErrorController : Controller
    {
        /// <summary>
        ///     Возвращает полное или частичное представление 400 ошибки.
        /// </summary>
        /// <returns>Полное или частичное представление 400 ошибки.</returns>
        [OutputCache(CacheProfile = CacheProfileName.BadRequest)]
        [Route("badrequest", Name = ErrorControllerRoute.GetBadRequest)]
        public ActionResult BadRequest()
        {
            return GetErrorView(HttpStatusCode.BadRequest, ErrorControllerView.BadRequest);
        }

        /// <summary>
        ///     Возвращает полное или частичное представление 403 ошибки.
        /// </summary>
        /// <returns>Полное или частичное представление 403 ошибки.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Forbidden)]
        [Route("forbidden", Name = ErrorControllerRoute.GetForbidden)]
        public ActionResult Forbidden()
        {
            return GetErrorView(HttpStatusCode.Forbidden, ErrorControllerView.Forbidden);
        }

        /// <summary>
        ///     Возвращает полное или частичное представление 500 ошибки.
        /// </summary>
        /// <returns>Полное или частичное представление 500 ошибки.</returns>
        [OutputCache(CacheProfile = CacheProfileName.InternalServerError)]
        [Route("internalservererror", Name = ErrorControllerRoute.GetInternalServerError)]
        public ActionResult InternalServerError()
        {
            return GetErrorView(HttpStatusCode.InternalServerError, ErrorControllerView.InternalServerError);
        }

        /// <summary>
        ///     Возвращает полное или частичное представление 405 ошибки.
        /// </summary>
        /// <returns>Полное или частичное представление 405 ошибки.</returns>
        [OutputCache(CacheProfile = CacheProfileName.MethodNotAllowed)]
        [Route("methodnotallowed", Name = ErrorControllerRoute.GetMethodNotAllowed)]
        public ActionResult MethodNotAllowed()
        {
            return GetErrorView(HttpStatusCode.MethodNotAllowed, ErrorControllerView.MethodNotAllowed);
        }

        /// <summary>
        ///     Возвращает полное или частичное представление 404 ошибки.
        /// </summary>
        /// <returns>Полное или частичное представление 404 ошибки.</returns>
        [OutputCache(CacheProfile = CacheProfileName.NotFound)]
        [Route("notfound", Name = ErrorControllerRoute.GetNotFound)]
        public ActionResult NotFound()
        {
           return GetErrorView(HttpStatusCode.NotFound, ErrorControllerView.NotFound);
        }

        /// <summary>
        ///     Возвращает полное или частичное представление 401 ошибки.
        /// </summary>
        /// <returns>Полное или частичное представление 401 ошибки.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Unauthorized)]
        [Route("unauthorized", Name = ErrorControllerRoute.GetUnauthorized)]
        public ActionResult Unauthorized()
        {
            return GetErrorView(HttpStatusCode.Unauthorized, ErrorControllerView.Unauthorized);
        }

        /// <summary>
        ///     Получить представление, полное или, если пришёл запрос от Ajax, частичное.
        /// </summary>
        /// <param name="status">Статус код ошибки</param>
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