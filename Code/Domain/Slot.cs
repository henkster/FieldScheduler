using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Slot : DomainObject<int>
    {
        public Slot()
        {
            Games = new List<Game>();
        }
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public virtual Field Field { get; set; }

        public virtual List<Game> Games { get; private set; }

        public bool IsAvailable
        {
            get { return Games.Count == 0 || Games.All(g => g.IsCanceled); }
        }
    }
}