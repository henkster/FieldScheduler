using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AdminOnly]
    [Authorize]
    public class FieldController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(FieldViewModel.LoadList(Context.Fields.Include("Slots").OrderBy(f => f.Description).ToList()));
        }

        public ActionResult Create()
        {
            var vm = new FieldCreateViewModel
                         {
                             Conflicts = FieldConflictViewModel.LoadList(Context.Fields)
                         };


            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(FieldCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var field = new Field
                              {
                                  AreRefereesRequired = vm.AreRefereesRequired,
                                  Size = vm.Size,
                                  Description = vm.Description,
                                  HasLights = vm.HasLights,
                                  AllowedActivities = BuildAllowedActivities(vm)
                              };

            foreach (FieldConflictViewModel possibleConflict in vm.Conflicts)
            {
                if (possibleConflict.IsConflict)
                {
                    field.AddConflict(Context.Fields.Find(possibleConflict.Id));
                }
            }

            Context.Fields.Add(field);

            Context.SaveChanges();

            TempData["message"] = "Field created.";

            return RedirectToAction("Index");
        }

        private Activities BuildAllowedActivities(FieldCreateViewModel vm)
        {
            Activities activities = 0;

            if (vm.AllowStateLeague) activities = activities | Activities.StateLeague;
            if (vm.AllowFriendly) activities = activities | Activities.Friendly;
            if (vm.AllowTraining) activities = activities | Activities.Training;

            return activities;
        }

        public ActionResult Delete(int id)
        {
            Field field = Context.Fields.SingleOrDefault(f => f.Id == id);

            if (field == null)
            {
                TempData["message"] = "That field cannot be found any more.";
                return RedirectToAction("Index");
            }
            if (field.Slots.Any())
            {
                TempData["message"] = "That field cannot removed since there are slots.";
                return RedirectToAction("Index");
            }

            Context.Fields.Remove(field);

            Context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}