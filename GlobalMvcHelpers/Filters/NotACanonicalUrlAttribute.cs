using System;
using System.Web.Mvc;

namespace GlobalMvcHelpers.Filters
{
    /// <summary>
    /// Указывает, что данный URL не каноничный. Если в конце /, возвращает 404 Not Found. 
    /// Может быть полезным при динамическом генерировании статичного контента. 
    /// Например, /Robots.txt/ должен быть /Robots.txt. Так же не нужно менять регистр букв.
    /// </summary>
    // ReSharper disable RedundantAttributeUsageProperty
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    // ReSharper restore RedundantAttributeUsageProperty
    public class NotACanonicalUrlAttribute : FilterAttribute, IAuthorizationFilter
    {
        private const char QueryCharacter = '?';
        private const char SlashCharacter = '/';

        /// <summary>
        /// Определяет присутствие / в конце URL и вызывает в случае найденного метод 
        /// <see cref="HandleTrailingSlashRequest"/>.
        /// </summary>
        /// <param name="filterContext">Объект, содержащий информацию, необхожимую для 
        /// <see cref="RequireHttpsAttribute"/> аттрибута.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="filterContext"/> равен <c>null</c>.</exception>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            // ReSharper disable once PossibleNullReferenceException
            var canonicalUrl = filterContext.HttpContext.Request.Url.ToString();
            var queryIndex = canonicalUrl.IndexOf(QueryCharacter);

            if (queryIndex == -1)
            {
                if (canonicalUrl[canonicalUrl.Length - 1] == SlashCharacter)
                {
                    HandleTrailingSlashRequest(filterContext);
                }
            }
            else
            {
                if (canonicalUrl[queryIndex - 1] == SlashCharacter)
                {
                    HandleTrailingSlashRequest(filterContext);
                }
            }
        }

        /// <summary>
        /// Обрабатывает HTTP запрос оканчивающегося на /.
        /// </summary>
        /// <param name="filterContext">Объект, содержащий информацию, необхожимую для 
        /// <see cref="RequireHttpsAttribute"/> аттрибута.</param>
        protected virtual void HandleTrailingSlashRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}
