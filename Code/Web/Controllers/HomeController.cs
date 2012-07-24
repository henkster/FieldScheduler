using System.Linq;
using System.Web.Mvc;
using Domain;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            Setting setting = Context.Settings.Single(s => s.Key == "home-page-message");

            return View((object)setting.Value);
        }

        public ActionResult SetupMode()
        {
            if (Context.Settings.Single(s => s.Key == "system-mode").Value != "setup")
            {
                return RedirectToAction("Index", "Home");
            }
            Setting setting = Context.Settings.Single(s => s.Key == "setup-mode-message");

            return View((object)setting.Value);
        }

        public ActionResult MaintenanceMode()
        {
            if (Context.Settings.Single(s => s.Key == "system-mode").Value != "maintenance")
            {
                return RedirectToAction("Index", "Home");
            }
            Setting setting = Context.Settings.Single(s => s.Key == "maintenance-mode-message");

            return View((object)setting.Value);
        }
    }
}
