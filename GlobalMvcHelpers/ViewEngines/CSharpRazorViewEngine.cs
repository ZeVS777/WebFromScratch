using System.Web.Mvc;

namespace GlobalMvcHelpers.ViewEngines
{
    /// <summary>
    /// Движок представлений, который используется для формирования разметки Web-страницы на стороне сервера, используя синтаксис ASP.NET Razor
    ///             и формат файлов C# .cshtml. <see cref="T:System.Web.Mvc.RazorViewEngine"/> поддерживает C# и VB и ищет файлы обоих фоматов .cshtml и .vbhtml.
    ///             Данная версия ищет только .cshtml файлы, таким образом ускоряя процесс нахождения нужного файла.
    /// 
    /// </summary>
    public class CSharpRazorViewEngine : RazorViewEngine
    {
        /// <summary>
        /// Инициализация класса <see cref="T:GlobalMvcHelpers.ViewEngines.CSharpRazorViewEngine"/>.
        /// 
        /// </summary>
        public CSharpRazorViewEngine()
        {
            // Пути поиска представлений Area
            this.AreaViewLocationFormats = new string[2]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            // Пути поиска мастер представлений Area
            this.AreaMasterLocationFormats = new string[2]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            // Пути поиска частичных представлений Area
            this.AreaPartialViewLocationFormats = new string[2]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            // Пути поиска представлений
            this.ViewLocationFormats = new string[2]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            // // Пути поиска мастер представлений
            this.MasterLocationFormats = new string[2]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            // Пути поиска частичных представлений
            this.PartialViewLocationFormats = new string[2]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            // Расширение файлов представлений
            this.FileExtensions = new string[1]
            {
                "cshtml"
            };
        }
    }
}