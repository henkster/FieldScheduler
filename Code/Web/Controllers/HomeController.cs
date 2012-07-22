using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Tennesee Soccer Club Managers Field Scheduler.";

            return View();
        }

        public ActionResult SetupMode()
        {
            return View();
        }

        public ActionResult MaintenanceMode()
        {
            return View();
        }

        public ActionResult StateLeagueMode()
        {
            return View();
        }
    }
}
