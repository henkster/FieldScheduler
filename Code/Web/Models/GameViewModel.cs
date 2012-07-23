using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class GameViewModel
    {
        public Slot Slot { get; set; }
        public Activities Activity { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

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

        public string TimeRange { get { return string.Format("{0}-{1}", Start, End); } }

        public string Field
        {
            get { return Slot.Field.Description; }
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