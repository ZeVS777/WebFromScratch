using System;
using System.Linq;
using System.Web.Mvc;

namespace GlobalMvcHelpers.Filters
{
    /// <summary>
    /// Для оптимизации SEO, должен быть единственный URL на один ресурс. 
    /// случай с окончанием на URL на слэш и без него трактуются как разные URL поисковыми маршрутами. 
    /// Данный фильтр перенаправляет все не канонические URL на их канонические эквиваленты. 
    /// Примечание: В основном, проблема может возникнуть из-за внешних ссылок с других сайтов.
    /// http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
    /// http://blogs.bing.com/webmaster/2012/01/26/moving-content-think-301-not-relcanonical).
    /// </summary>
    // ReSharper disable RedundantAttributeUsageProperty
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    // ReSharper restore RedundantAttributeUsageProperty
    public class RedirectToCanonicalUrlAttribute : FilterAttribute, IAuthorizationFilter
    {
        #region Fields

        private const char QueryCharacter = '?';
        private const char SlashCharacter = '/';

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует класс <see cref="RedirectToCanonicalUrlAttribute" />.
        /// </summary>
        /// <param name="appendTrailingSlash">Если <c>true</c> добавлять / в конец URL, иначе избавляться от него 
        /// slashes.</param>
        /// <param name="lowercaseUrls">Если <c>true</c> все URL будут написаны прописными буквами.</param>
        public RedirectToCanonicalUrlAttribute(
            bool appendTrailingSlash,
            bool lowercaseUrls)
        {
            AppendTrailingSlash = appendTrailingSlash;
            LowercaseUrls = lowercaseUrls;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Получить значение настройки для / в конце URL.
        /// </summary>
        /// <value>
        /// <c>true</c>, если добавляется /; иначе стирается, <c>false</c>.
        /// </value>
        public bool AppendTrailingSlash { get; }

        /// <summary>
        /// Получить значение написания URL.
        /// </summary>
        /// <value>
        /// <c>true</c>, если прописными; иначе, <c>false</c>.
        /// </value>
        public bool LowercaseUrls { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Определяет, что HTTP запрос содержит канонический URL, используя <see cref="TryGetCanonicalUrl"/>, 
        /// Если нет, вызывает метод <see cref="HandleNonCanonicalRequest"/>.
        /// </summary>
        /// <param name="filterContext">Объект, содержащий информацию, необхожимую для 
        /// <see cref="RedirectToCanonicalUrlAttribute"/> аттрибута.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filterContext"/> параметр равен <c>null</c>.</exception>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            string canonicalUrl;
            if (!TryGetCanonicalUrl(filterContext, out canonicalUrl))
            {
                HandleNonCanonicalRequest(filterContext, canonicalUrl);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Определяе каноничность URl и он не такой, то на выход выдаёт canonicalUL.
        /// </summary>
        /// <param name="filterContext">Объект, содержащий информацию, необхожимую для 
        /// <see cref="RedirectToCanonicalUrlAttribute" /> аттрибута.</param>
        /// <param name="canonicalUrl">Каноничная строка URL.</param>
        /// <returns><c>true</c>, если заданный URL каноничный, иначе <c>false</c>.</returns>
        protected virtual bool TryGetCanonicalUrl(AuthorizationContext filterContext, out string canonicalUrl)
        {
            // Предполагаем, что URL каноничен
            var isCanonical = true;
            // Получаем URL из запроса
            var url = filterContext.HttpContext.Request.Url;
            // ReSharper disable once PossibleNullReferenceException
            canonicalUrl = url.ToString();

            // Если это не домашняя страница (корень приложения).
            // Поисковыми машинами корень приложения воспринимается как один адрес
            // вне зависимости от следующего знака /
            if (url.AbsolutePath.Length > 1)
            {
                // Ищем конец URL до символа ?
                var queryIndex = canonicalUrl.IndexOf(QueryCharacter);
                
                if (queryIndex == -1)
                {
                    var hasTrailingSlash = canonicalUrl[canonicalUrl.Length - 1] == SlashCharacter;

                    if (AppendTrailingSlash)
                    {
                        // Если нет символа /, то добавляем его.
                        if (!hasTrailingSlash && !HasNoNotACanonicalUrlAttribute(filterContext))
                        {
                            canonicalUrl += SlashCharacter;
                            isCanonical = false;
                        }
                    }
                    else
                    {
                        // Убрать / с конца URL.
                        if (hasTrailingSlash)
                        {
                            canonicalUrl = canonicalUrl.TrimEnd(SlashCharacter);
                            isCanonical = false;
                        }
                    }
                }
                else
                {
                    var hasTrailingSlash = canonicalUrl[queryIndex - 1] == SlashCharacter;

                    if (AppendTrailingSlash)
                    {
                        // Если нет символа /, то добавляем его перед ?.
                        if (!hasTrailingSlash && !HasNoNotACanonicalUrlAttribute(filterContext))
                        {
                            canonicalUrl = canonicalUrl.Insert(queryIndex, SlashCharacter.ToString());
                            isCanonical = false;
                        }
                    }
                    else
                    {
                        // Убрать / с конца URL перед ?.
                        if (hasTrailingSlash)
                        {
                            canonicalUrl = canonicalUrl.Remove(queryIndex - 1, 1);
                            isCanonical = false;
                        }
                    }
                }
            }

            // Если не требуется проверка нижнего регистра URL
            if (!LowercaseUrls) return isCanonical;

            if (!canonicalUrl.Any(character => char.IsUpper(character) && !HasNoNotACanonicalUrlAttribute(filterContext)))
                return isCanonical;

            // Если URL содержит строчные буквы, то седлать их прописными
            canonicalUrl = canonicalUrl.ToLower();

            return false;
        }

        /// <summary>
        /// Обрабатывает HTTP запросы для неканоничных URL. Редиректит со статусом 301 Permanent Redirect по каноничному URL.
        /// </summary>
        /// <param name="filterContext">Объект, содержащий информацию, необхожимую для 
        /// <see cref="RedirectToCanonicalUrlAttribute" /> аттрибута.</param>
        /// <param name="canonicalUrl">Каноничный URL.</param>
        protected virtual void HandleNonCanonicalRequest(AuthorizationContext filterContext, string canonicalUrl)
        {
            filterContext.Result = new RedirectResult(canonicalUrl, true);
        }

        /// <summary>
        /// Определяет, есть ли над методом или его контроллером аттрибут <see cref="NotACanonicalUrlAttribute"/> 
        /// </summary>
        /// <param name="filterContext">Контекст фильтра.</param>
        /// <returns><c>true</c>, если <see cref="NotACanonicalUrlAttribute"/> аттрибут определён, иначе 
        /// <c>false</c>.</returns>
        protected virtual bool HasNoNotACanonicalUrlAttribute(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.IsDefined(typeof(NotACanonicalUrlAttribute), false) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(NotACanonicalUrlAttribute), false);
        }

        #endregion
    }
}