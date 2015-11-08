using System;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using GlobalMvcHelpers.ViewEngines;

namespace WebFromScratch
{
    public class Global : System.Web.HttpApplication
    {
        #region Events
        // Событие, срабатываемое перед запуском основного приложения
        protected void Application_Start(object sender, EventArgs e)
        {
            ConfigureViewEngines();
            ConfigureAntiForgeryTokens();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Конфигурирование движков представлений (View Engines).
        /// </summary>
        private static void ConfigureViewEngines()
        {
            /*     
            *   Изначально, ASP.NET MVC включает всевозможные движки, для любого языка
            *   программирования (.cshtml, .vbhtml). Здесь же происхожит регистрирование
            *   только нужного, тем самым получая выигрышь во времени поиска нужного представления.
            */
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());
        }

        /// <summary>
        /// Настройка маркера проверки подлинности. Смотри
        /// https://msdn.microsoft.com/ru-ru/library/system.web.helpers.antiforgery(v=vs.111).aspx
        /// </summary>
        private static void ConfigureAntiForgeryTokens()
        {
            /*
            *   Маркер используется при защите от межсайтовой подделки запроса с форм.
            *   Смотри https://ru.wikipedia.org/wiki/%D0%9C%D0%B5%D0%B6%D1%81%D0%B0%D0%B9%D1%82%D0%BE%D0%B2%D0%B0%D1%8F_%D0%BF%D0%BE%D0%B4%D0%B4%D0%B5%D0%BB%D0%BA%D0%B0_%D0%B7%D0%B0%D0%BF%D1%80%D0%BE%D1%81%D0%B0
            *   Переименование cookie маркера с "__RequestVerificationToken" на "_fT"
            *   позволяет добавить защищённости засчёт незнания. К сожалению, нет возможности
            *   изменить имя формы input маркера, жёстко записано в @Html.AntiForgeryToken и 
            *   аттрибут ValidationAntiforgeryTokenAttribute как __RequestVerificationToken.
            *   <input name="__RequestVerificationToken" type="hidden" value="..." />
            */
            AntiForgeryConfig.CookieName = "_fT";

            // TODO: При включении SSL раскомментировать 
            // следующую строку, чтобы убедиться, что
            // cookie маркера будет требовать SSL. 
            // AntiForgeryConfig.RequireSsl = true;
        }
    }
}