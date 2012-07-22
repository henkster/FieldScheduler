using System;
using System.Web.Mvc;

namespace Web.Helpers.ExtensionMethods
{
    public static class HtmlExtensions
    {
        public static bool IsTrue(this HtmlHelper helper, string key)
        {
            return Convert.ToBoolean(helper.ViewData[key]);
        }
    }
}