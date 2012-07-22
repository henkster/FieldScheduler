using System;

namespace Domain
{
    public class Slot : DomainObject<int>
    {
        public DateTime StartDateTime { get; set; }

        public SlotDuration Duration
        {
            get { return (SlotDuration) Enum.ToObject(typeof (SlotDuration), DurationAsInt); }
            set { DurationAsInt = (int) value; }
        }

        public int DurationAsInt { get; set; }

        public DateTime EndTime
        {
            get { return StartDateTime.AddMinutes((int)Duration); }
        }

        public virtual Field Field { get; set; }
    }
}