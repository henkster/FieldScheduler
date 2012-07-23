using System.Linq;
using System.Web.Mvc;
using Domain;
using Services;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AdminOnly]
    public class SlotController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(SlotViewModel.LoadList(Context.Slots.OrderBy(s => s.StartDateTime).ToList()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SlotCreationViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var field = Context.Fields.Find(vm.FieldId);

            new SlotCreator(Context).Create(field,
                                            vm.DayOfWeek,
                                            vm.StartDate.Date,
                                            vm.EndDate.Date,
                                            vm.StartTime.TimeOfDay,
                                            vm.EndTime.TimeOfDay,
                                            (SlotDuration)vm.MinutesPerSlot);

            @TempData["message"] = "Slots created.";

            return RedirectToAction("Index");
        }
    }
}