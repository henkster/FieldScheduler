using System.Web.Mvc;
using Domain;
using Web.Controllers;

namespace Web.Helpers
{
    public class AllowOnlyAttribute : ActionFilterAttribute
    {
        private readonly Roles[] _roles;

        public AllowOnlyAttribute(params Roles[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var appController = filterContext.Controller as ApplicationController;

            if (appController == null || appController.LoggedInUser == null || !appController.LoggedInUser.IsInAny(_roles))
            {
                filterContext.Result = new RedirectResult("/");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}