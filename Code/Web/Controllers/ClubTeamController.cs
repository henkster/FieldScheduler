using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Helpers;
using Web.Models;

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
            var vm = new ClubTeamCreateEditViewModel();

            ClubTeamCreateEditViewModel.InitializeList(vm, Context.Divisions.OrderBy(d => d.Age).ThenBy(d => d.Gender).ToList());

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(ClubTeamCreateEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ClubTeamCreateEditViewModel.InitializeList(vm, Context.Divisions.OrderBy(d => d.Age).ThenBy(d => d.Gender).ToList());
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

        public ActionResult Edit(int id)
        {
            var team = Context.ClubTeams.Find(id);

            return View(ClubTeamCreateEditViewModel.Load(team, Context.Divisions));
        }

        [HttpPost]
        public ActionResult Edit(ClubTeamCreateEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ClubTeamCreateEditViewModel.InitializeList(vm, Context.Divisions);

                return View(vm);
            }

            var team = Context.ClubTeams.Find(vm.Id);

            team.Name = vm.Name;
            team.Division = Context.Divisions.Find(vm.DivisionId);
            team.Level = vm.Level;

            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}