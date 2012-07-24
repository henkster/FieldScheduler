using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Models;

namespace Web.Controllers
{
    public class ExternalTeamController : ApplicationController
    {
        public ActionResult ByClub(int id)
        {
            return View(Context.ExternalTeams.Where(t => t.Club.Id == id).ToList());
        }

        public ActionResult Create(string returnTo)
        {
            var vm = TeamCreateViewModel.LoadFromSelect(Context.Clubs.OrderBy(c => c.Name).ToList(),
                                                        Context.Divisions.OrderBy(d => d.Age).ToList());

            vm.ReturnTo = returnTo;

            ViewBag.TeamNames = FindTeamsByClub();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(TeamCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TeamNames = FindTeamsByClub();

                return View(vm);
            }

            if (Context.ExternalTeams.Any(t => t.Name == vm.NewTeamName && t.Club.Id == vm.ClubId))
            {
                TempData["message"] = "That team already exists.";
                return Redirect(vm.ReturnTo);
            }
            Club club = Context.Clubs.SingleOrDefault(c => c.Id == vm.ClubId);

            if (club == null)
            {
                TempData["message"] = "System error.  Club cannot be found.";
                return Redirect(vm.ReturnTo);
            }

            Division division = Context.Divisions.SingleOrDefault(d => d.Id == vm.DivisionId);

            if (division == null)
            {
                TempData["message"] = "System error.  Division cannot be found.";
                return Redirect(vm.ReturnTo);
            }

            var team = new ExternalTeam
                           {
                               Club = club,
                               Division = division,
                               Name = vm.NewTeamName,
                               CityState = vm.CityState,
                               ContactName = vm.ContactName,
                               ContactPhoneNumber = vm.ContactPhoneNumber,
                               ContactEmailAddress = vm.ContactEmailAddress,
                               Level = vm.Level
                           };

            Context.ExternalTeams.Add(team);

            Context.SaveChanges();

            return Redirect(vm.ReturnTo);
        }

        private IEnumerable<string> FindTeamsByClub()
        {
            Club club = Context.Clubs.OrderBy(c => c.Name).Take(1).FirstOrDefault();

            foreach (ExternalTeam team in Context.ExternalTeams.Where(t => t.Club.Id == club.Id).OrderBy(t => t.Name))
            {
                yield return team.Name;
            }
        }
    }
}