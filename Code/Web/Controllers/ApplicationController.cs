﻿
using System.Linq;
using System.Web.Mvc;
using Data;
using Domain;

namespace Web.Controllers
{
    public abstract class ApplicationController : Controller
    {
        private Context _context;
        private User _loggedInUser;

        protected Context Context
        {
            get { return _context ?? (_context = new Context()); }
        }

        public User LoggedInUser
        {
            get
            {
                if (_loggedInUser == null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        _loggedInUser = Context.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                    }
                }
                return _loggedInUser;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["show-admin"] = LoggedInUser != null && LoggedInUser.IsAdmin;

            CheckMode(filterContext);

            base.OnActionExecuting(filterContext);
        }

        private void CheckMode(ActionExecutingContext filterContext)
        {
            Setting setting = Context.Settings.Single(s => s.Key == "system-mode");

            ViewData["system-mode"] = setting.Value;

            if (setting.Value != "public" && LoggedInUser != null && !LoggedInUser.IsAdmin && !Request.Url.ToString().Contains("Mode") && !Request.Url.ToString().Contains("Account"))
            {
                filterContext.Result = RedirectToAction(setting.Value == "setup" ? "SetupMode" : "MaintenanceMode", "Home");
            }
        }
    }
}
