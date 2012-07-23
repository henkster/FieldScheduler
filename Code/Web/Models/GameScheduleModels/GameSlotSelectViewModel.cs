using System;
using System.Collections.Generic;
using Domain;

namespace Web.Models.GameScheduleModels
{
    public class GameSlotSelectViewModel
    {
        public string Activity { get; set; }
        public string Size { get; set; }
        public string Date { get; set; }
        public IEnumerable<SlotSummaryViewModel> Slots { get; set; }

        public GameSlotSelectViewModel(string activity, string size, string date, List<Slot> slots)
        {
            Activity = activity;
            Size = size;
            Date = date;
            Slots = SlotSummaryViewModel.LoadList(slots);
        }
    }
}