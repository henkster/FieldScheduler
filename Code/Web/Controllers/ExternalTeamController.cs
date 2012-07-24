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

        public ActionResult Create(string activity, string size, string date, int slotId)
        {
            var vm = TeamCreateViewModel.LoadFromSelect(activity,
                                                        size,
                                                        date,
                                                        slotId,
                                                        Context.Clubs.OrderBy(c => c.Name).ToList(),
                                                        Context.Divisions.OrderBy(d => d.Age).ToList());

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(TeamCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (Context.ExternalTeams.Any(t => t.Name == vm.NewTeamName && t.Club.Id == vm.ClubId))
            {
                TempData["message"] = "That team already exists.";
                return RedirectToAction("Select", "Game", new { vm.Activity, vm.Size, vm.Date, vm.SlotId });
            }
            Club club = Context.Clubs.SingleOrDefault(c => c.Id == vm.ClubId);

            if (club == null)
            {
                TempData["message"] = "System error.  Club cannot be found.";
                return RedirectToAction("Select", "Game", new { vm.Activity, vm.Size, vm.Date, vm.SlotId });
            }

            Division division = Context.Divisions.SingleOrDefault(d => d.Id == vm.DivisionId);

            if (division == null)
            {
                TempData["message"] = "System error.  Division cannot be found.";
                return RedirectToAction("Select", "Game", new { vm.Activity, vm.Size, vm.Date, vm.SlotId });
            }

            var team = new ExternalTeam
                           {
                               Club = club,
                               Division = division,
                               Name = vm.NewTeamName,
                               CityState = vm.CityState,
                               ContactName = vm.ContactName,
                               ContactPhoneNumber = vm.ContactPhoneNumber,
                               ContactEmailAddress = vm.ContactEmailAddress
                           };

            Context.ExternalTeams.Add(team);

            Context.SaveChanges();

            return RedirectToAction("Select", "Game", new { vm.Activity, vm.Size, vm.Date, vm.SlotId });
        }
    }
}