using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Field : DomainObject<int>
    {
        public Field()
        {
            Slots = new List<Slot>();
            FieldsProhibitingThis = new List<Field>();
            FieldsProhibitedByThis = new List<Field>();
        }
        public string Description { get; set; }

        public FieldSize Size
        {
            get { return (FieldSize) Enum.ToObject(typeof (FieldSize), SizeAsInt); }
            set { SizeAsInt = (int) value; }
        }

        public int SizeAsInt { get; set; }
        public bool AreRefereesRequired { get; set; }
        public bool HasLights { get; set; }

        public Activities AllowedActivities
        {
            get { return (Activities)Enum.ToObject(typeof(Activities), AllowedActivityAsInt); }
            set { AllowedActivityAsInt = (int)value; }
        }

        public int AllowedActivityAsInt { get; set; }

        public virtual List<Slot> Slots { get; set; }

        public bool CanBeDeleted()
        {
            return !Slots.Any();
        }

        /// <summary>
        /// Fields that cannot be used at the same time as this, often due to cross-lining.
        /// </summary>
        public virtual List<Field> FieldsProhibitingThis { get; private set; }

        public virtual List<Field> FieldsProhibitedByThis { get; private set; }
 
        public void AddConflict(Field conflictingField)
        {
            FieldsProhibitingThis.Add(conflictingField);
            FieldsProhibitedByThis.Add(conflictingField);
        }

        public bool HasPossibleConflict
        {
            get
            {
                if (FieldsProhibitingThis.Count == 0) return false;
                return FieldsProhibitingThis.Any(f => !f.CanBeDeleted());
            }
        }
    }
}