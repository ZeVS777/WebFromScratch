﻿ // ReSharper disable once CheckNamespace
namespace WebFromScratch.Constants
{
    /// <summary>
    ///     Класс, хранящий строковые константы маршрутов контроллера Error
    /// </summary>
    public static class ErrorControllerRoute
    {
        public const string GetBadRequest = ControllerName.Error + "GetBadRequest";
        public const string GetForbidden = ControllerName.Error + "GetForbidden";
        public const string GetInternalServerError = ControllerName.Error + "GetInternalServerError";
        public const string GetMethodNotAllowed = ControllerName.Error + "GetMethodNotAllowed";
        public const string GetNotFound = ControllerName.Error + "GetNotFound";
        public const string GetUnauthorized = ControllerName.Error + "Unauthorized";
    }
}