using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public Slot Slot { get; set; }
        public Activities Activity { get; set; }
        public DateTime Date { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public User ScheduledBy { get; set; }
        public DateTime ScheduledOn { get; set; }
        public User CanceledBy { get; set; }
        public DateTime? CanceledOn { get; set; }

        public string Age
        {
            get
            {
                if (Team1.Division.Age == Team2.Division.Age) return string.Format("U{0}", Team1.Division.Age);
                return string.Format("U{0}/U{1}", Team1.Division.Age, Team2.Division.Age);
            }
        }

        [Display(Name = "Are Ref Needed?")]
        public bool AreRefereesNeeded { get; set; }

        [Display(Name = "Are Refs Confirmed?")]
        public bool AreRefereesConfirmed { get; set; }

        public string Reason
        {
            get
            {
                switch (Activity)
                {
                    case Activities.Friendly:
                        return "Friendly";
                    case Activities.StateLeague:
                        return "State League";
                    case Activities.Training:
                        return "Training";
                }
                return "Unknown";
            }
        }

        public string FormattedTime { get { return string.Format("{0}-{1}", Slot.StartDateTime.ToShortTimeString(), Slot.EndDateTime.ToShortTimeString()); } }
        public string FormattedDate { get { return string.Format("{0}, {1}", Slot.StartDateTime.DayOfWeek, Slot.StartDateTime.ToString("MMMM d")); } }

        public string Size
        {
            get
            {
                switch (Slot.Field.Size)
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
        }

        public string Team1Name
        {
            get { return Team1.FullName; }
        }

        public string Team2Name
        {
            get { return Team2.FullName; }
        }

        public string Field
        {
            get { return Slot.Field.Description; }
        }

        public string ScheduledByName
        {
            get { return ScheduledBy.Name; }
        }

        public static IEnumerable<GameViewModel> LoadList(IEnumerable<Game> games)
        {
            foreach (Game game in games) yield return Load(game);
        }

        public static GameViewModel Load(Game game)
        {
            return Mapper.Map<Game, GameViewModel>(game);
        }
    }
}