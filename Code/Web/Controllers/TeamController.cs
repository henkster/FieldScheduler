using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
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

    }
}
