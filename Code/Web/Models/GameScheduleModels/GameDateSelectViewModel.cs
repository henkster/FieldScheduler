using System;
using System.Collections.Generic;

namespace Web.Models.GameScheduleModels
{
    public class GameDateSelectViewModel
    {
        public string Activity { get; set; }
        public string Size { get; set; }
        public List<DateTime> SlotDates { get; set; }

        public GameDateSelectViewModel(string activity, string size, List<DateTime> slotDates)
        {
            Activity = activity;
            Size = size;
            SlotDates = slotDates;
        }
    }
}