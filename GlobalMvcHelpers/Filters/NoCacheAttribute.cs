using System;
using System.Web.Mvc;

namespace GlobalMvcHelpers.Filters
{
    /// <summary>
    /// Аттрибут, используемый, чтобы пометить метод, чей вывод не должен кэшироваться.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class NoCacheAttribute : OutputCacheAttribute
    {
        /// <summary>
        /// Инициализирует класс <see cref="NoCacheAttribute"/>.
        /// </summary>
        public NoCacheAttribute()
        {
            NoStore = true;
            // Duration = 0 по дефолту.
            // VaryByParam = "*" по дефолту.
        }
    }
}