using System;
using System.Web.Mvc;
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
        /// Конфигурирование Движков представлений (View Engines).
        ///     Изначально, ASP.NET MVC включает всевозможные движки, для любого языка
        ///     программирования (.cshtml, .vbhtml). Здесь же происхожит регистрирование
        ///     только нужного, тем самым получая выигрышь во времени поиска нужного прежставления.
        /// </summary>
        private static void ConfigureViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());
        }
    }
}