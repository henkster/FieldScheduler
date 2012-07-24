using System.Linq;
using System.Web.Mvc;
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
            return View(FieldViewModel.LoadList(Context.Fields.ToList()));
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}