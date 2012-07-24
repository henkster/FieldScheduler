using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain;

namespace Web.Controllers
{
    [Authorize]
    public class TeamController : ApplicationController
    {
        public JsonResult GetByDivision(int division)
        {
            var teams = new List<SelectListItem>();

            foreach(var team in Context.ExternalTeams.Where(t => t.Division.Id == division).OrderBy(t => t.Name))
            {
                teams.Add(new SelectListItem { Text = team.FullName, Value = team.Id.ToString() });
            }

            return Json(teams);
        }

        public JsonResult GetByClub(int clubId)
        {
            Club club = Context.Clubs.FirstOrDefault(c => c.Id == clubId);

            var teams = new List<string>();

            foreach (ExternalTeam team in Context.ExternalTeams.Where(t => t.Club.Id == club.Id).OrderBy(t => t.Name))
            {
                teams.Add(team.Name);
            }

            return Json(teams);
        }

    }
}
