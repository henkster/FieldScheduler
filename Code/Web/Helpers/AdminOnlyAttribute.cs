using System.Web.Mvc;
using Domain;
using Web.Controllers;

namespace Web.Helpers
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var appController = filterContext.Controller as ApplicationController;

            if (appController == null || appController.LoggedInUser == null || !appController.LoggedInUser.IsIn(Roles.Admin))
            {
                filterContext.Result = new RedirectResult("/");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}