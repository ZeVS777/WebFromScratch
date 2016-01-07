using System;
using System.Web;

namespace WebFromScratch.HttpModules
{
    /// <summary>
    ///     Модуль, убирающий заголовок Server
    /// </summary>
    public class RemoveServerResponseHeader : IHttpModule
    {
        /// <summary>
        ///     Инициализирует модуль и подготавливает его для обработки запросов.
        /// </summary>
        /// <param name="context">Объект <see cref="T:System.Web.HttpApplication"/>, предоставляющий доступ к методам, свойствам и событиям, являющимся общими для всех объектов в приложении ASP.NET. </param>
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        /// <summary>
        ///     Удаляет ресурсы (кроме памяти), используемые модулем.
        /// </summary>
        public void Dispose() { }


        /// <summary>
        ///     Обработчик события, происходящего в ASP.NET перед отправкой HTTP=-заголовков киенту.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            // как говориться здесь: http://www.asp.net/aspnet/overview/web-development-best-practices/what-not-to-do-in-aspnet-and-what-to-do-instead#presend
            // использование OnPreSendRequestHeaders не желательно
            // нужны другие решение такие, как native-code module для IIS
            // здесь так реализовано лишь для того, чтобы показать, что для защиты по не знанию
            // заголовок Server не должен сообщать о реально используемых технологиях.
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
}