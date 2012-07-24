using System.Linq;
using System.Web.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    [AdminOnly]
    [Authorize]
    public class ClubTeamController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(Context.ClubTeams.ToList());
        }
    }
}