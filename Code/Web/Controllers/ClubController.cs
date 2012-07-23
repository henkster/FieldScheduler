using System.Linq;
using System.Web.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    [AdminOnly]
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

        public ActionResult Create(string activity, string size, string date, int slotid)
        {
            throw new System.NotImplementedException();
        }
    }
}