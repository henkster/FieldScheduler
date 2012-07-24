using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    public class ClubController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(Context.Clubs.ToList());
        }

        public ActionResult Edit(int id)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Create(string activity, string size, string date, int slotId)
        {
            var vm = ClubCreateViewModel.LoadFromSelect(activity,
                                            size,
                                            date,
                                            slotId);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(ClubCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Club club = Context.Clubs.SingleOrDefault(c => c.Name == vm.Name);

            if (club != null)
            {
                TempData["message"] = "Club already exists";
                return RedirectToAction("Create", "ExternalTeam", new { vm.Activity, vm.Size, vm.Date, vm.SlotId });
            }

            var newClub = new Club
            {
                Name = vm.Name,
                CityState = vm.CityState
            };

            Context.Clubs.Add(newClub);

            Context.SaveChanges();

            return RedirectToAction("Create", "ExternalTeam", new { vm.Activity, vm.Size, vm.Date, vm.SlotId });
        }
    }
}