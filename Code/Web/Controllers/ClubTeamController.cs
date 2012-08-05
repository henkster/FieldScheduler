using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Services;
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

            TempData["message"] = "TSC team created.";

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

            TempData["message"] = "TSC team updated.";

            return RedirectToAction("Index");
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                //// extract only the fielname
                //var fileName = Path.GetFileName(file.FileName);
                //// store the file inside ~/App_Data/uploads folder
                //var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                //file.SaveAs(path);

                //StreamReader sr = new StreamReader(file.InputStream);

                new ClubTeamImporter(Context).Import(file.InputStream);

                TempData["message"] = "Upload successful.";
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");   
        }
    }
}