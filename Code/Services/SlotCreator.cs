using System;
using System.Linq;
using Data;
using Domain;

namespace Services
{
    public class SlotCreator
    {
        private readonly Context _context;

        public SlotCreator(Context context)
        {
            _context = context;
        }

        public void Create(Field field, 
            DayOfWeek dayOfWeek, 
            DateTime startDate, 
            DateTime endDate, 
            TimeSpan startTime, 
            TimeSpan endTime, 
            SlotDuration duration, 
            bool allowFriendlies,
            bool allowStateLeague,
            bool allowTraining)
        {
            var currentDay = startDate.Date;
            var endDay = endDate.Date;

            while (currentDay <= endDay)
            {
                TimeSpan currentTime = startTime;

                while (currentTime.Add(new TimeSpan(0, (int) duration, 0)) <= endTime)
                {
                    if (currentDay.DayOfWeek == dayOfWeek && IsSlotOpen(field, currentDay, currentTime, duration))
                    {
                        var slot = new Slot
                                       {
                                           Field = field,
                                           StartDateTime = currentDay.Add(currentTime),
                                           EndDateTime = currentDay.Add(currentTime).AddMinutes((int) duration),
                                           AllowedActivities = MapAllowedActivities(allowFriendlies, allowStateLeague, allowTraining)
                                       };

                        _context.Slots.Add(slot);
                    }
                    currentTime = currentTime.Add(new TimeSpan(0, (int) duration, 0));
                }

                currentDay = currentDay.AddDays(1);
            }

            _context.SaveChanges();
        }

        private Activities MapAllowedActivities(bool allowFriendlies, bool allowStateLeague, bool allowTraining)
        {
            Activities activities = 0;

            if (allowFriendlies) activities = activities | Activities.Friendly;
            if (allowStateLeague) activities = activities | Activities.StateLeague;
            if (allowTraining) activities = activities | Activities.Training;

            return activities;
        }

        private bool IsSlotOpen(Field field, DateTime day, TimeSpan time, SlotDuration duration)
        {
            var start = day.Add(time);
            var end = start.AddMinutes((int) duration);

            return !_context.Slots.Any(
                s =>
                s.Field.Id == field.Id &&
                ((s.StartDateTime <= start && s.EndDateTime > start) || (s.StartDateTime > start && s.StartDateTime < end)));
        }
    }
}