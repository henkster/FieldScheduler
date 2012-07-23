using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Domain;

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

            if (Context.ExternalTeams.Any(t => t.Name == vm.NewClubName && t.Club.Id == vm.ClubId))
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
                               Name = vm.NewClubName,
                               ContactName = vm.ContactName,
                               ContactPhoneNumber = vm.ContactPhoneNumber,
                               ContactEmailAddress = vm.ContactEmailAddress
                           };

            Context.ExternalTeams.Add(team);

            Context.SaveChanges();

            return RedirectToAction("Select", "Game", new { vm.Activity, vm.Size, vm.Date, vm.SlotId });
        }
    }

    public class TeamCreateViewModel
    {
        public string Activity { get; set; }
        public string Size { get; set; }
        public string Date { get; set; }
        public int SlotId { get; set; }
        public int ClubId { get; set; }
        public List<SelectListItem> ClubList { get; set; }
        public int DivisionId { get; set; }
        public List<SelectListItem> DivisionList { get; set; }

        [Required]
        public string NewClubName { get; set; }

        [Required]
        public string ContactName { get; set; }
        
        [Required]
        public string ContactEmailAddress { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }

        public static TeamCreateViewModel LoadFromSelect(string activity, string size, string date, int slotId, List<Club> clubs, List<Division> divisions)
        {
            var vm = new TeamCreateViewModel
                         {
                             Activity = activity,
                             Size = size,
                             Date = date,
                             SlotId = slotId,
                             ClubList = new List<SelectListItem>(),
                             DivisionList = new List<SelectListItem>()
                         };

            foreach (Club club in clubs)
            {
                vm.ClubList.Add(new SelectListItem {Text= club.Name, Value = club.Id.ToString()});
            }

            foreach (Division division in divisions)
            {
                vm.DivisionList.Add(new SelectListItem { Text = division.Name, Value = division.Id.ToString() });
            }

            return vm;
        }
    }
}