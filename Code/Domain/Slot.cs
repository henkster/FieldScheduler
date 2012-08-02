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
            get
            {
                if (Field == null || (Games.Count > 0 && Games.Any(g => !g.IsCanceled))) return false;

                foreach (Field field in Field.FieldsProhibitingThis)
                {
                    foreach (Slot slot in field.Slots)
                    {
                        if (
                            slot.Games.Any(
                                g =>
                                !g.IsCanceled &&
                                ((g.Slot.StartDateTime >= StartDateTime && g.Slot.StartDateTime < EndDateTime) ||
                                 (g.Slot.EndDateTime > StartDateTime && g.Slot.EndDateTime <= EndDateTime) ||
                                 (g.Slot.StartDateTime <= StartDateTime && g.Slot.EndDateTime >= EndDateTime))))
                            return false;
                    }
                }
                return true;
            }
        }
    }
}