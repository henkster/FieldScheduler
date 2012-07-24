﻿using System;
using System.Collections.Generic;
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
            return View(GameViewModel.LoadList(Context.Games.Where(g => g.ScheduledBy.Id == LoggedInUser.Id && !g.CanceledOn.HasValue)));
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
            var fieldSize = (int)MapFieldSize(size);
            var activityInt = (int)MapActivity(activity);

            var slotDates = (from slot in Context.Slots
                             where slot.Field.SizeAsInt == fieldSize && ((slot.Field.AllowedActivityAsInt & activityInt) == activityInt )
                            select EntityFunctions.TruncateTime(slot.StartDateTime).Value).Distinct();

            return View("Date", new GameDateSelectViewModel(activity, size, slotDates.ToList()));
        }

        [ActionName("Slot")]
        public ActionResult Schedule(string activity, string size, string date)
        {
            var gameDate = new DateTime(int.Parse(date.Substring(4, 4)),
                                        int.Parse(date.Substring(0, 2)),
                                        int.Parse(date.Substring(2, 2)));

            var fieldSize = (int)MapFieldSize(size);

            var slots = from slot in Context.Slots
                        where EntityFunctions.TruncateTime(slot.StartDateTime) == gameDate && slot.Field.SizeAsInt == fieldSize
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
                return RedirectToAction("Slot", "Game", new { activity, size, date });
            }

            if (!slot.IsAvailable)
            {
                TempData["message"] = "That slot is no longer available.";
                return RedirectToAction("Slot", "Game", new { activity, size, date });
            }

            var division = Context.Divisions.Single(d => d.Age == 8 && d.Gender == "Boys");
            var teams = new List<Team>();
            foreach (var team in Context.ExternalTeams.Where(t => t.Division.Id == division.Id).OrderBy(t => t.Name)) teams.Add(team);
            
            return View(new SelectionViewModel(teams, Context.Divisions.OrderBy(d => d.Age).ToList(), null)
                            {
                                Activity = activity,
                                Size = size,
                                Date = date,
                                SlotId = slotId,
                                Description = SlotSummaryViewModel.CreateDescription(slot)
                            });
        }

        [ActionName("Select")]
        [HttpPost]
        public ActionResult Schedule(SelectionViewModel vm)
        {
            Slot slot = Context.Slots.Find(vm.SlotId);

            if (slot == null)
            {
                TempData["message"] = "A system issue prevented that slot from being found.";
                return RedirectToAction("Slot", "Game", new {vm.Activity, vm.Size, vm.Date});
            }

            if (!slot.IsAvailable)
            {
                TempData["message"] = "That slot is no longer available.";
                return RedirectToAction("Slot", "Game", new { vm.Activity, vm.Size, vm.Date });
            }

            var game = new Game
                           {
                               Activity = MapActivity(vm.Activity),
                               ScheduledBy = LoggedInUser,
                               ScheduledOn = DateTime.Now,
                               Slot = slot,
                               Team1 = Context.ExternalTeams.Find(vm.Team1),
                               Team2 = Context.ExternalTeams.Find(vm.Team2)
                           };

            Context.Games.Add(game);

            //slot.Games.Add(game);
            
            Context.SaveChanges();

            TempData["message"] = "Game scheduled!";
            return RedirectToAction("Slot", "Game", new { vm.Activity, vm.Size, vm.Date });
        }

        public ActionResult Delete(int id)
        {
            Game game = Context.Games.Include("Slot").Include("Team1").Include("Team2").SingleOrDefault(g => g.Id == id);

            if (game == null)
            {
                TempData["message"] = "The system could not find that game.";
                return RedirectToAction("Summary");
            }

            game.CanceledBy = LoggedInUser;
            game.CanceledOn = DateTime.Now;

            Context.SaveChanges();

            TempData["message"] = "Game successfully canceled!";
            return RedirectToAction("Summary");
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

        private FieldSize MapFieldSize(string size)
        {
            switch (size)
            {
                case "11v11":
                    return FieldSize.ElevenVsEleven;
                case "8v8":
                    return FieldSize.EightVsEight;
                case "6v6":
                    return FieldSize.SixVsSix;
            }
            throw new ArgumentOutOfRangeException(string.Format("Field size '{0}' not found.", size));
        }
    }
}