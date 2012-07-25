using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Helpers.ExtensionMethods
{
    public static class HtmlExtensions
    {
        public static bool IsTrue(this HtmlHelper helper, string key)
        {
            return Convert.ToBoolean(helper.ViewData[key]);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string imageLocation, string action, string controller, object routeValues, object htmlAttributes, string alt, string title)
        {
            var url = new UrlHelper(helper.ViewContext.RequestContext);

            var aBuilder = new TagBuilder("a");

            // Add attributes
            aBuilder.MergeAttribute("href", url.Action(action, controller, routeValues)); //form target URL
            aBuilder.InnerHtml = string.Format(string.Format("<img src='{0}' alt='{1}' title='{2}' /></a>", url.Content(string.Format("~/Images/{0}", imageLocation)), alt, title));
            aBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(aBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString YesNo(this HtmlHelper helper, bool? test, string nullText = "--")
        {
            string result;

            if (!test.HasValue) result = nullText;
            else result = test.Value ? "Yes" : "No";

            return MvcHtmlString.Create(result);
        }
    }
}