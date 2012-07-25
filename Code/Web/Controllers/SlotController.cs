using System.Linq;
using System.Web.Mvc;
using Domain;
using Services;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AdminOnly]
    [Authorize]
    public class SlotController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(SlotViewModel.LoadList(Context.Slots.Include("Games").OrderBy(s => s.StartDateTime).ToList()));
        }

        public ActionResult Create()
        {
            var vm = new SlotCreateViewModel();

            SlotCreateViewModel.InitializeLists(vm, Context.Fields.OrderBy(f => f.Description));

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(SlotCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                SlotCreateViewModel.InitializeLists(vm, Context.Fields.OrderBy(f => f.Description));
                return View(vm);
            }

            var field = Context.Fields.Find(vm.FieldId);

            new SlotCreator(Context).Create(field,
                                            vm.DayOfWeek.Value,
                                            vm.StartDate.Value.Date,
                                            vm.EndDate.Value.Date,
                                            vm.StartTime.Value.TimeOfDay,
                                            vm.EndTime.Value.TimeOfDay,
                                            (SlotDuration)vm.MinutesPerSlot);

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
    }
}