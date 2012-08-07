using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Services;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AllowOnly(Roles.Admin)]
    [Authorize]
    public class SlotController : ApplicationController
    {
        public ActionResult Index()
        {
            var vm = new SlotListViewModel();
            
            vm.Slots = SlotViewModel.LoadList(Context.Slots.Include("Games").OrderBy(s => s.StartDateTime).ToList());

            SlotListViewModel.InitializeList(vm, Context.Fields);

            return View(vm);
        }

        public ActionResult FieldSelect()
        {
            return View(Context.Fields.OrderBy(f => f.Description));
        }

        public ActionResult Create(int id)
        {
            var field = Context.Fields.Find(id);

            var vm = new SlotCreateViewModel();

            if (field.Size == FieldSize.SixVsSix) vm.MinutesPerSlot = (int)SlotDuration.SevetyFive;
            if (field.Size == FieldSize.EightVsEight) vm.MinutesPerSlot = (int)SlotDuration.Ninety;
            if (field.Size == FieldSize.ElevenVsEleven) vm.MinutesPerSlot = (int)SlotDuration.OneHundredTwenty;

            vm.FieldId = field.Id;
            vm.FieldDescription = field.Description;
            vm.AllowFriendlies = (field.AllowedActivities & Activities.Friendly) == Activities.Friendly;
            vm.AllowStateLeague = (field.AllowedActivities & Activities.StateLeague) == Activities.StateLeague;
            vm.AllowTraining = (field.AllowedActivities & Activities.Training) == Activities.Training;

            SlotCreateViewModel.InitializeLists(vm);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(SlotCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                SlotCreateViewModel.InitializeLists(vm);
                return View(vm);
            }

            var field = Context.Fields.Find(vm.FieldId);

            new SlotCreator(Context).Create(field,
                                            vm.DayOfWeek.Value,
                                            vm.StartDate.Value.Date,
                                            vm.EndDate.Value.Date,
                                            vm.StartTime.Value.TimeOfDay,
                                            vm.EndTime.Value.TimeOfDay,
                                            (SlotDuration)vm.MinutesPerSlot,
                                            vm.AllowFriendlies,
                                            vm.AllowStateLeague,
                                            vm.AllowTraining);

            TempData["message"] = "Slots created.";

            return RedirectToAction("Create", new {id = field.Id});
        }

        public ActionResult Delete(int id)
        {
            Slot slot = Context.Slots.SingleOrDefault(s => s.Id == id);

            if (slot == null)
            {
                TempData["message"] = "That slot cannot be found any more.";
                return RedirectToAction("Index");
            }
            if (slot.Games.Any(s => !s.IsCanceled))
            {
                TempData["message"] = "That slot cannot removed since there is a game .";
                return RedirectToAction("Index");
            }

            Context.Slots.Remove(slot);

            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ForField(int fieldId)
        {
            var field = Context.Fields.SingleOrDefault(f => f.Id == fieldId);

            if (field == null) return RedirectToAction("Index");

            var vm = new SlotListViewModel();

            vm.Slots = 
                SlotViewModel.LoadList(
                    Context.Slots.Include("Games").Where(s => s.Field.Id == field.Id).OrderBy(s => s.StartDateTime).ToList());

            vm.FieldId = fieldId;

            SlotListViewModel.InitializeList(vm, Context.Fields);

            return View("Index", vm);
        }

        public ActionResult MultiDelete()
        {
            return View(SlotMultiDeleteViewModel.LoadList(Context.Fields.OrderBy(f => f.Description)));
        }

        [HttpDelete]
        public ActionResult MultiDelete(SlotMultiDeleteViewModel vm)
        {
            foreach (SlotDeleteViewModel field in vm.Slots)
            {
                if (field.Monday) RemoveSlots(field.FieldId, DayOfWeek.Monday);
                if (field.Tuesday) RemoveSlots(field.FieldId, DayOfWeek.Tuesday);
                if (field.Wednesday) RemoveSlots(field.FieldId, DayOfWeek.Wednesday);
                if (field.Thursday) RemoveSlots(field.FieldId, DayOfWeek.Thursday);
                if (field.Friday) RemoveSlots(field.FieldId, DayOfWeek.Friday);
                if (field.Saturday) RemoveSlots(field.FieldId, DayOfWeek.Saturday);
                if (field.Sunday) RemoveSlots(field.FieldId, DayOfWeek.Sunday);
            }

            Context.SaveChanges();

            TempData["message"] = "Deleted";

            return RedirectToAction("Index");
        }

        private void RemoveSlots(int fieldId, DayOfWeek dayOfWeek)
        {
            var firstSunday = new DateTime(1753, 1, 7);

            foreach (Slot slot in Context.Slots.Where(s => s.Field.Id == fieldId && EntityFunctions.DiffDays(firstSunday, s.StartDateTime) % 7 == (int)dayOfWeek))
            {
                Context.Slots.Remove(slot);
            }
        }
    }

    public class SlotMultiDeleteViewModel
    {
        public IList<SlotDeleteViewModel> Slots { get; set; }

        public static SlotMultiDeleteViewModel LoadList(IEnumerable<Field> fields)
        {
            var vm = new SlotMultiDeleteViewModel();

            vm.Slots = new List<SlotDeleteViewModel>();

            foreach (Field field in fields)
            {
                vm.Slots.Add(new SlotDeleteViewModel
                {
                    FieldId = field.Id,
                    FieldDescription = field.Description
                });
            }

            return vm;
        }

    }

    public class SlotDeleteViewModel
    {
        public int FieldId { get; set; }
        public string FieldDescription { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}