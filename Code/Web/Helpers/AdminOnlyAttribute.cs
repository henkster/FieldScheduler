using System.Web.Mvc;
using Web.Controllers;

namespace Web.Helpers
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var appController = filterContext.Controller as ApplicationController;

            if (appController == null || appController.LoggedInUser == null || !appController.LoggedInUser.IsAdmin)
            {
                filterContext.Result = new RedirectResult("/");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}