using System.Linq;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class ManagerController : ApplicationController
    {
        public ActionResult Club()
        {
            ViewBag.ManagerShowLabel = "Show External Contacts";
            ViewBag.ManagerAction = "External";
            ViewBag.ManagerListTitle = "TSC Managers/Contacts";

            return View("List", ManagerViewModel.LoadList(
                Context.ClubTeams.OrderBy(t => t.Division.Age).ThenBy(t => t.Division.Gender).ThenBy(t => t.Name)));
        }

        public ActionResult External()
        {
            ViewBag.ManagerShowLabel = "Show TSC Contacts";
            ViewBag.ManagerAction = "Club";
            ViewBag.ManagerListTitle = "External Managers/Contacts";

            return View("List", ManagerViewModel.LoadList(
                Context.ExternalTeams.OrderBy(t => t.Division.Age).ThenBy(t => t.Division.Gender).ThenBy(t => t.Name)));
        }
    }
}
