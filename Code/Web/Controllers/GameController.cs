﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Services;
using Web.Helpers;
using Web.Models;
using Web.Models.GameScheduleModels;

namespace Web.Controllers
{
    [Authorize]
    public class GameController : ApplicationController
    {
        [AllowOnly(Roles.Admin)]
        public ActionResult AllGames()
        {
            ViewData["hide-game-change-buttons"] = true;
            ViewData["game-summary-title"] = "All Scheduled Games";

            return View("Summary", GameViewModel.LoadList(Context.Games.Where(g => !g.CanceledOn.HasValue).OrderBy(g => g.Slot.StartDateTime)));
        }
        [AllowOnly(Roles.Admin)]
        public ActionResult AllCanceledGames()
        {
            ViewData["hide-game-change-buttons"] = true;
            ViewData["game-summary-title"] = "All Canceled Games";

            return View("Summary", GameViewModel.LoadList(Context.Games.Where(g => g.CanceledOn.HasValue).OrderBy(g => g.Slot.StartDateTime)));
        }
        public ActionResult Summary()
        {
            return View(GameViewModel.LoadList(Context.Games.Where(g => g.ScheduledBy.Id == LoggedInUser.Id && !g.CanceledOn.HasValue).OrderBy(g => g.Slot.StartDateTime)));
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
                             where slot.Field.SizeAsInt == fieldSize && ((slot.AllowedActivityAsInt & activityInt) == activityInt )
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

            var activityInt = (int)MapActivity(activity);

            var slots = from slot in Context.Slots
                        where EntityFunctions.TruncateTime(slot.StartDateTime) == gameDate && slot.Field.SizeAsInt == fieldSize && (slot.AllowedActivityAsInt & activityInt) == activityInt
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

            var teams = new List<Team>();

            ClubTeam myTeam = LoggedInUser.Teams.Count > 0 ? LoggedInUser.Teams[0] : null;

            if (myTeam != null)
            {
                foreach (
                    var team in
                        Context.ClubTeams.Where(t => t.Division.Age == myTeam.Division.Age && t.Division.Gender == myTeam.Division.Gender).OrderBy(
                            t => t.Club.Name).ThenBy(t => t.Name)) teams.Add(team);
                foreach (
                    var team in
                        Context.ExternalTeams.Where(t => t.Division.Age == myTeam.Division.Age && t.Division.Gender == myTeam.Division.Gender).OrderBy(
                            t => t.Club.Name).ThenBy(t => t.Name)) teams.Add(team);
            }
            else
            {
                foreach (
                    var team in
                        Context.ClubTeams.Where(t => t.Division.Age == 8 && t.Division.Gender == "Boys").OrderBy(
                            t => t.Club.Name).ThenBy(t => t.Name)) teams.Add(team);
                foreach (
                    var team in
                        Context.ExternalTeams.Where(t => t.Division.Age == 8 && t.Division.Gender == "Boys").OrderBy(
                            t => t.Club.Name).ThenBy(t => t.Name)) teams.Add(team);
            }

            ViewBag.GameSelectionTitle = "Schedule Game - Details";
            ViewBag.GameSelectionButton = "Schedule";
            ViewBag.ReturnTo = Url.Action("Select", "Game", new {activity, size, date, slotId});
            ViewBag.CancelUrl = Url.Action("Slot", "Game", new {activity, size, date});

            return View(new SelectionViewModel(teams, teams, Context.Divisions.OrderBy(d => d.Age).ThenBy(d => d.Gender).ToList())
                            {
                                Activity = activity,
                                Size = size,
                                Date = date,
                                SlotId = slotId,
                                Description = SlotSummaryViewModel.CreateDescription(slot),
                                AreRefereesRequired = slot.Field.AreRefereesRequired,
                                Division1Id = myTeam != null ? myTeam.Division.Id : 0,
                                Division2Id = myTeam != null ? myTeam.Division.Id : 0,
                                Team1Id = myTeam != null ? myTeam.Id : 0,
                                Team2Id = myTeam != null ? myTeam.Id : 0
                            });
        }

        [ActionName("Select")]
        [HttpPost]
        public ActionResult Schedule(SelectionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GameSelectionTitle = "Schedule Game - Details";
                ViewBag.GameSelectionButton = "Schedule";
                ViewBag.ReturnTo = Url.Action("Select", "Game", new {vm.Activity, vm.Size, vm.Date, vm.SlotId });
                ViewBag.CancelUrl = Url.Action("Slot", "Game", new { vm.Activity, vm.Size, vm.Date });

                return View(vm);
            }

            GameCreationResult result = new GameCreator(Context, Server.MapPath("/bin")).Create(vm.SlotId,
                                                                        vm.Team1Id,
                                                                        vm.Team2Id,
                                                                        MapActivity(vm.Activity),
                                                                        vm.Notes,
                                                                        vm.AreRefereesNeeded,
                                                                        LoggedInUser);

            if (result == GameCreationResult.SlotNotAvailable)
            {
                TempData["message"] = "That slot is no longer available.";
                return RedirectToAction("Slot", "Game", new { vm.Activity, vm.Size, vm.Date });
            }

            TempData["message"] = "Game scheduled!";
            return RedirectToAction("Slot", "Game", new { vm.Activity, vm.Size, vm.Date });
        }

        public ActionResult Delete(int id)
        {
            Game game = Context.Games.Include("Slot").Include("Team1").Include("Team2").SingleOrDefault(g => g.Id == id && g.ScheduledBy.Id == LoggedInUser.Id);

            if (game == null)
            {
                TempData["message"] = "The system could not find that game for your user account.";
                return RedirectToAction("Summary");
            }

            game.CanceledBy = LoggedInUser;
            game.CanceledOn = DateTime.Now;

            Context.SaveChanges();

            new RefereeEmailer(Context, Server.MapPath("~/bin")).EmailCanceled(game);

            TempData["message"] = "Game successfully canceled!";
            return RedirectToAction("Summary");
        }

        public ActionResult Edit(int id)
        {
            Game game = Context.Games.SingleOrDefault(g => g.Id == id && g.ScheduledBy.Id == LoggedInUser.Id);

            if (game == null)
            {
                TempData["message"] = "Game cannot be found for your user account.";
                return RedirectToAction("Summary");
            }

            var teams1 = new List<Team>();
            foreach (var team in Context.ClubTeams.Where(t => t.Division.Id == game.Team1.Division.Id).OrderBy(t => t.Name)) teams1.Add(team);
            foreach (var team in Context.ExternalTeams.Where(t => t.Division.Id ==  game.Team1.Division.Id).OrderBy(t => t.Name)) teams1.Add(team);

            var teams2 = new List<Team>();
            foreach (var team in Context.ClubTeams.Where(t => t.Division.Id == game.Team2.Division.Id).OrderBy(t => t.Name)) teams2.Add(team);
            foreach (var team in Context.ExternalTeams.Where(t => t.Division.Id == game.Team2.Division.Id).OrderBy(t => t.Name)) teams2.Add(team);

            ViewBag.GameSelectionTitle = "Game Edit";
            ViewBag.GameSelectionButton = "Update";
            ViewBag.ReturnTo = Url.Action("Edit", "Game", new { game.Id });
            ViewBag.CancelUrl = Url.Action("Summary");

            return View("Select", new SelectionViewModel(teams1, teams2, Context.Divisions.OrderBy(d => d.Age).ToList(), game.Team1, game.Team2, game.Team1.Division, game.Team2.Division)
            {
                Activity = game.Activity.ToString(),
                Size = MapSize(game.Slot.Field.Size),
                Date = game.Slot.StartDateTime.ToShortDateString(),
                SlotId = game.Slot.Id,
                Description = SlotSummaryViewModel.CreateDescription(game.Slot),
                Id = game.Id,
                Team1Id = game.Team1.Id,
                Team2Id = game.Team2.Id,
                Division1Id = game.Team1.Division.Id,
                Division2Id = game.Team2.Division.Id,
                Notes = game.Notes,
                AreRefereesRequired = game.Slot.Field.AreRefereesRequired,
                AreRefereesNeeded = game.AreRefereesNeeded
            });
        }

        private string MapSize(FieldSize size)
        {
            switch (size)
            {
                case FieldSize.ElevenVsEleven:
                    return "11 v 11";
                case FieldSize.EightVsEight:
                    return "8 v 8";
                case FieldSize.SixVsSix:
                    return "6 v 6";
            }
            return "Unknown";
        }

        [HttpPost]
        public ActionResult Edit(SelectionViewModel vm)
        {
            Game game = Context.Games.Include("Slot").SingleOrDefault(g => g.Id == vm.Id && g.ScheduledBy.Id == LoggedInUser.Id);

            if (game == null)
            {
                TempData["message"] = "Game cannot be found for your user account.";
                return RedirectToAction("Summary");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.GameSelectionTitle = "Game Edit";
                ViewBag.GameSelectionButton = "Update";
                ViewBag.ReturnTo = Url.Action("Edit", "Game", new { vm.Id });
                ViewBag.CancelUrl = Url.Action("Summary");

                return View("Select", vm);
            }

            Team team1 = Context.ExternalTeams.SingleOrDefault(t => t.Id == vm.Team1Id) ??
                         (Team) Context.ClubTeams.SingleOrDefault(t => t.Id == vm.Team1Id);

            Team team2 = Context.ExternalTeams.SingleOrDefault(t => t.Id == vm.Team2Id) ??
                         (Team) Context.ClubTeams.SingleOrDefault(t => t.Id == vm.Team2Id);

            if (team1 == null || team2 == null)
            {
                TempData["message"] = "One of the teams cannot be found.";
                return RedirectToAction("Summary");
            }

            game.Team1 = team1;
            game.Team2 = team2;
            game.Notes = vm.Notes;

            Context.SaveChanges();

            new RefereeEmailer(Context, Server.MapPath("~/bin")).EmailModified(game);

            return RedirectToAction("Summary");
        }

        [AllowOnly(Roles.Referee)]
        public ActionResult Confirm()
        {
            return View(GameViewModel.LoadList(Context.Games.Where(g => !g.CanceledOn.HasValue).OrderBy(g => g.Slot.StartDateTime)));
        }

        [AllowOnly(Roles.Referee)]
        [HttpPut]
        public ActionResult Confirm(int id)
        {
            Game game = Context.Games.Include("Team1").Include("Team2").Include("ScheduledBy").SingleOrDefault(g => g.Id == id);

            if (game == null)
            {
                TempData["message"] = "Game could not be found.";
                return RedirectToAction("Confirm");
            }

            game.AreRefereesConfirmed = true;

            Context.SaveChanges();

            TempData["message"] = "Game confirmed!";

            return RedirectToAction("Confirm");
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