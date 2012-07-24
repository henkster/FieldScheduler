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

        public string DateFormatted
        {
            get { if (!Date.Contains("/"))
            {
                return DateTime.Parse(string.Format("{0}/{1}/{2}", Date.Substring(0, 2), Date.Substring(2, 2), Date.Substring(4, 4))).ToString("M/d/yy");
            }
                return Date;
            }
        }

        public GameSlotSelectViewModel(string activity, string size, string date, List<Slot> slots)
        {
            Activity = activity;
            Size = size;
            Date = date;
            Slots = SlotSummaryViewModel.LoadList(slots);
        }
    }
}