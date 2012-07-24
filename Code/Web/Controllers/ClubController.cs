using System.Collections.Generic;
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

        public ActionResult Create(string returnTo)
        {
            var vm = new ClubCreateViewModel {ReturnTo = returnTo};

            ViewBag.ExistingClubs = FindExistingClubs();

            return View(vm);
        }

        private IEnumerable<Club> FindExistingClubs()
        {
            foreach (Club club in Context.Clubs.OrderBy(c => c.Name)) yield return club;
        }

        [HttpPost]
        public ActionResult Create(ClubCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ExistingClubs = FindExistingClubs();
                return RedirectToAction("Create", "ExternalTeam", new { vm.ReturnTo });
            }

            Club club = Context.Clubs.SingleOrDefault(c => c.Name == vm.Name);

            if (club != null)
            {
                TempData["message"] = "Club already exists";
                return RedirectToAction("Create", "ExternalTeam", new {vm.ReturnTo});
            }

            var newClub = new Club
            {
                Name = vm.Name,
                CityState = vm.CityState
            };

            Context.Clubs.Add(newClub);

            Context.SaveChanges();

            return RedirectToAction("Create", "ExternalTeam", new { vm.ReturnTo });
        }
    }
}