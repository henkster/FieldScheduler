using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AllowOnly(Roles.Admin)]
    [Authorize]
    public class FieldController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(FieldViewModel.LoadList(Context.Fields.Include("Slots").OrderBy(f => f.Description).ToList()));
        }

        public ActionResult Create()
        {
            var vm = new FieldCreateEditViewModel
                         {
                             Conflicts = FieldConflictViewModel.LoadList(Context.Fields)
                         };


            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(FieldCreateEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Conflicts = FieldConflictViewModel.LoadList(Context.Fields);
                
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

        private Activities BuildAllowedActivities(FieldCreateEditViewModel vm)
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

        public ActionResult Edit(int id)
        {
            var field = Context.Fields.Find(id);

            var vm = new FieldCreateEditViewModel
                         {
                             Id = field.Id,
                             AllowFriendly = (field.AllowedActivities & Activities.Friendly) == Activities.Friendly,
                             AllowStateLeague =
                                 (field.AllowedActivities & Activities.StateLeague) == Activities.StateLeague,
                             AllowTraining = (field.AllowedActivities & Activities.Training) == Activities.Training,
                             AreRefereesRequired = field.AreRefereesRequired,
                             Description = field.Description,
                             HasLights = field.HasLights,
                             Size = field.Size,
                             Conflicts = FieldConflictViewModel.LoadList(Context.Fields, field)
                         };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(FieldCreateEditViewModel vm)
        {
            var field = Context.Fields.Find(vm.Id);

            if (!ModelState.IsValid)
            {
                vm.Conflicts = FieldConflictViewModel.LoadList(Context.Fields, field);
                
                return View(vm);
            }

            field.AreRefereesRequired = vm.AreRefereesRequired;
            field.Size = vm.Size;
            field.Description = vm.Description;
            field.HasLights = vm.HasLights;
            field.AllowedActivities = BuildAllowedActivities(vm);

            foreach (FieldConflictViewModel possibleConflict in vm.Conflicts)
            {
                if (possibleConflict.IsConflict && !field.FieldsProhibitingThis.Any(f => f.Id == possibleConflict.Id))
                {
                    field.AddConflict(Context.Fields.Find(possibleConflict.Id));
                }
                else if(!possibleConflict.IsConflict && field.FieldsProhibitingThis.Any(f => f.Id == possibleConflict.Id))
                {
                    field.RemoveConflict(Context.Fields.Find(possibleConflict.Id));
                }
            }

            Context.SaveChanges();

            TempData["message"] = "Field updated.";

            return RedirectToAction("Index");
        }
    }
}