using System;
using System.Web.Mvc;

namespace GlobalMvcHelpers.Filters
{
    /// <summary>
    /// Не применять к query string правила аттрибута <see cref="RedirectToCanonicalUrlAttribute"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class NoLowercaseQueryStringAttribute : FilterAttribute
    {
    }
}