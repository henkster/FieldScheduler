using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class SlotViewModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public SlotDuration Duration { get; set; }
        public Field Field { get; set; }
        public bool CanBeDeleted { get; set; }
        public string GameScheduledBy { get; set; }

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

        public string Size
        {
             get
             {
                 switch (Field.Size)
                 {
                     case FieldSize.SixVsSix:
                         return "6v6";
                     case FieldSize.EightVsEight:
                         return "8v8";
                     case FieldSize.ElevenVsEleven:
                         return "11v11";
                 }
                 return "??";
             }
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
            SlotViewModel vm = Mapper.Map<Slot, SlotViewModel>(slot);

            Game game = slot.Games.SingleOrDefault(g => !g.IsCanceled);

            vm.CanBeDeleted = game == null;

            vm.GameScheduledBy = game != null ? game.ScheduledBy.Name : string.Empty;

            return vm;
        }
    }
}