using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Helpers;

namespace Web.Controllers
{
    [AllowOnly(Roles.Admin)]
    [Authorize]
    public class ClubTeamController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(Context.ClubTeams.ToList());
        }

        public ActionResult Create()
        {
            var vm = new ClubTeamCreateViewModel();

            ClubTeamCreateViewModel.InitializeList(vm, Context.Divisions.OrderBy(d => d.Age).ThenBy(d => d.Gender).ToList());

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(ClubTeamCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ClubTeamCreateViewModel.InitializeList(vm, Context.Divisions.OrderBy(d => d.Age).ThenBy(d => d.Gender).ToList());
                return View(vm);
            }

            var team = new ClubTeam();

            team.Name = vm.Name;
            team.Division = Context.Divisions.Find(vm.DivisionId);
            team.Level = vm.Level;

            Context.ClubTeams.Add(team);

            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }

    public class ClubTeamCreateViewModel
    {
        public string Name { get; set; }
        public int DivisionId { get; set; }
        public Level Level { get; set; }

        public SelectList DivisionList { get; set; }

        public static void InitializeList(ClubTeamCreateViewModel vm, IEnumerable<Division> clubTeams)
        {
            vm.DivisionList = new SelectList(clubTeams, "Id", "Name");
        }
    }
}