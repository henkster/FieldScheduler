using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class GameController : ApplicationController
    {
        public ActionResult Summary()
        {
            return View(GameViewModel.LoadList(Context.Games));
        }

        public ActionResult Schedule()
        {
            throw new System.NotImplementedException();
        }
    }
}