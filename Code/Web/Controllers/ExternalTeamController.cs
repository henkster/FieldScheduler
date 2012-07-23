using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ExternalTeamController : ApplicationController
    {
        public ActionResult ByClub(int id)
        {
            return View(Context.ExternalTeams.Where(t => t.Club.Id == id).ToList());
        }

        public ActionResult Create(string activity, string size, string date, int slotId)
        {
            return RedirectToAction("Select", "Game", new {activity, size, date, slotId});
        }
    }
}