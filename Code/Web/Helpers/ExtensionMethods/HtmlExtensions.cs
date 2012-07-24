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

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string imageLocation, string action, string controller, object routeValues, string alt, string title)
        {
            var url = new UrlHelper(helper.ViewContext.RequestContext);

            return MvcHtmlString.Create(string.Format("<a href='{0}'><img src='{1}' alt='{2}' title='{3}' /></a>",
                                                      url.Action(action, controller, routeValues),
                                                      url.Content(string.Format("~/Images/{0}", imageLocation)),
                                                      alt,
                                                      title));
        }
    }
}