using System.Linq;
using System.Web.Mvc;
using Domain;

namespace Web.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            Setting setting = Context.Settings.Single(s => s.Key == "home-page-message");

            return View((object)setting.Value);
        }

        public ActionResult SetupMode()
        {
            Setting setting = Context.Settings.Single(s => s.Key == "setup-mode-message");

            return View((object)setting.Value);
        }

        public ActionResult MaintenanceMode()
        {
            Setting setting = Context.Settings.Single(s => s.Key == "maintenance-mode-message");

            return View((object)setting.Value);
        }
    }
}
