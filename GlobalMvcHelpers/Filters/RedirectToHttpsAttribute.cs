using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GlobalMvcHelpers.Filters
{
    /// <summary>
    /// Аттрибут заставляющий HTTP запрос пойти через HTTPS, выполняя редирект со статусом 302 Temporary redirect.
    /// Данный фильтр позволяет выбрать тип редиректа 301 Permanent redirect или a 302 temporary redirect. 
    /// Если метод может выполняться только используя HTTPS, то следует выбрать 301 permanent redirect. 
    /// <see cref="RequireHttpsAttribute"/> выдаёт ошибку <see cref="InvalidOperationException"/> если метод запроса не GET, что возвращает 500 Internal Server 
    /// Error клиенту. Данный фильтр возврашает в такой ситуации 405 Method Not Allowed, что является более подходящим.
    /// </summary>
    // ReSharper disable RedundantAttributeUsageProperty
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    // ReSharper restore RedundantAttributeUsageProperty
    public class RedirectToHttpsAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Инициализирует класс <see cref="RedirectToHttpsAttribute"/>.
        /// </summary>
        /// <param name="permanent">если <c>true</c>, редирект будет с кодом 301 permanent; иначе, 
        /// <c>false</c>.</param>
        public RedirectToHttpsAttribute(bool permanent)
        {
            Permanent = permanent;
        }

        /// <summary>
        /// Получает значение, как выполнять перенаправление.
        /// </summary>
        /// <value>
        /// <c>true</c>, если перенаправление должно быть со статусом 301 permanent; иначе, <c>false</c>.
        /// </value>
        public bool Permanent { get; }

        /// <summary>
        /// Определяет, является ли протокол запроса HTTPS и, если это не так, вызывает 
        /// метод <see cref="HandleNonHttpsRequest"/>.
        /// </summary>
        /// <param name="filterContext">Объект, содержащий информацию, необхожимую для 
        /// <see cref="RequireHttpsAttribute"/> аттрибута.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="filterContext"/> parameter is <c>null</c>.</exception>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                HandleNonHttpsRequest(filterContext);
            }
        }

        /// <summary>
        /// Обработчик незащищённых HTTP запросов.
        /// </summary>
        /// <param name="filterContext">Объект, содержащий информацию, необхожимую для 
        /// <see cref="RequireHttpsAttribute"/> аттрибута.</param>
        /// <exception cref="HttpException">Метод запроса не правильный. 
        /// Ошибка HTTP 405 Method Not Allowed.</exception>
        protected virtual void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            // Перенаправляет только GET запросы.
            if (!string.Equals(
                filterContext.HttpContext.Request.HttpMethod,
                WebRequestMethods.Http.Get,
                StringComparison.OrdinalIgnoreCase))
            {
                // Иначе возвращается 405 Method Not Allowed.
                throw new HttpException((int)HttpStatusCode.Forbidden, "Forbidden");
            }

            // ReSharper disable once PossibleNullReferenceException
            string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(url, Permanent);
        }
    }
}