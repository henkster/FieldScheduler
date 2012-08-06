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

            return RedirectToAction("Index");
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

            SlotListViewModel.InitializeList(vm, Context.Fields);

            return View("Index", vm);
        }
    }
}