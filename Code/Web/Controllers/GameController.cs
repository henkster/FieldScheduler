using System;
using System.Data.Objects;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Web.Models;
using Web.Models.GameScheduleModels;

namespace Web.Controllers
{
    public class GameController : ApplicationController
    {
        public ActionResult Summary()
        {
            return View(GameViewModel.LoadList(Context.Games));
        }

        [ActionName("Activity")]
        public ActionResult Schedule() // TODO Convert this mess to a massive VM
        {
            return View("Activity");
        }

        [ActionName("Size")]
        public ActionResult Schedule(string activity)
        {
            return View("Size", new GameSizeSelectViewModel(activity));
        }

        [ActionName("Date")]
        public ActionResult Schedule(string activity, string size)
        {
            var slotDates = (from slot in Context.Slots
                            select EntityFunctions.TruncateTime(slot.StartDateTime).Value).Distinct();

            return View("Date", new GameDateSelectViewModel(activity, size, slotDates.ToList()));
        }

        [ActionName("Slot")]
        public ActionResult Schedule(string activity, string size, string date)
        {
            var gameDate = new DateTime(int.Parse(date.Substring(4, 4)),
                                        int.Parse(date.Substring(0, 2)),
                                        int.Parse(date.Substring(2, 2)));

            var slots = from slot in Context.Slots
                         where EntityFunctions.TruncateTime(slot.StartDateTime) == gameDate
                         select slot;

            return View("Slot", new GameSlotSelectViewModel(activity, size, date, slots.ToList()));
        }

        [ActionName("Select")]
        public ActionResult Schedule(string activity, string size, string date, int slotId)
        {
            Slot slot = Context.Slots.Find(slotId);

            if (slot == null)
            {
                TempData["message"] = "A system issue prevented that slot from being found.";
                return RedirectToAction("Slot", "Game", new {activity, size, date});
            }

            if (!slot.IsAvailable)
            {
                TempData["message"] = "That slot is no longer available.";
                return RedirectToAction("Slot", "Game", new { activity, size, date });
            }

            var game = new Game
                           {
                               Activity = MapActivity(activity),
                               ScheduledBy = LoggedInUser,
                               ScheduledOn = DateTime.Now,
                               Slot = slot,
                               Team1 = Context.ClubTeams.Find(2),
                               Team2 = Context.ExternalTeams.Find(1)
                           };

            Context.Games.Add(game);

            Context.GetValidationErrors();

            Context.SaveChanges();

            TempData["message"] = "Game scheduled!";
            return RedirectToAction("Slot", "Game", new { activity, size, date });
        }

        private Activities MapActivity(string activity)
        {
            switch (activity)
            {
                case "friendly" :
                    return Activities.Friendly;
                case "state-league":
                    return Activities.StateLeague;
                case "training":
                    return Activities.Training;
            }
            throw new ArgumentOutOfRangeException(string.Format("Activity '{0}' not found.", activity));
        }
    }
}