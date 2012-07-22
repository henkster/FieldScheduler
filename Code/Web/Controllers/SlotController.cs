using System.Linq;
using System.Web.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AdminOnly]
    public class SlotController : ApplicationController
    {
        public ActionResult Index()
        {
            return View(SlotViewModel.LoadList(Context.Slots.ToList()));
        }
    }
}