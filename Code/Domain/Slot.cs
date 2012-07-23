using System;

namespace Domain
{
    public class Slot : DomainObject<int>
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public virtual Field Field { get; set; }

        public virtual Game Game { get; set; }

        public bool IsAvailable
        {
            get { return Game == null; }
        }
    }
}