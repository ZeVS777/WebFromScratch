 // ReSharper disable once CheckNamespace
namespace WebFromScratch.Resources.Constants
{
    /// <summary>
    ///     Класс, хранящий строковые константы маршрутов контроллера Home
    /// </summary>
    public static class HomeControllerRoute
    {
        public const string GetIndex = ControllerName.Home + "GetIndex";
        public const string GetAbout = ControllerName.Home + "GetAbout";
        public const string GetManifestJson = ControllerName.Home + "GetManifestJson";
        public const string GetBrowserConfigXml = ControllerName.Home + "GetBrowserConfigXml";
        public const string GetFeed = ControllerName.Home + "GetFeed";
    }
}