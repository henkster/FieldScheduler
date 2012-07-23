using System;
using System.Collections.Generic;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class SlotViewModel
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public SlotDuration Duration { get; set; }
        public Field Field { get; set; }

        public string Date
        {
            get { return StartDateTime.ToShortDateString(); }
        }

        public string StartTime
        {
            get { return StartDateTime.ToShortTimeString(); }
        }

        public string EndTime
        {
            get { return EndDateTime.ToShortTimeString(); }
        }

        public string Location
        {
            get { return Field.Description; }
        }

        public DayOfWeek DayOfWeek
        {
            get { return StartDateTime.DayOfWeek; }
        }

        public string AllowedActivitiesFormatted
        {
            get
            {
                // TODO make this pleasant
                return Field.AllowedActivities.ToString(); 
            }
        }

        public static IEnumerable<SlotViewModel> LoadList(IEnumerable<Slot> slots)
        {
            foreach (Slot slot in slots) yield return Load(slot);
        }

        public static SlotViewModel Load(Slot slot)
        {
            return Mapper.Map<Slot, SlotViewModel>(slot);
        }
    }
}