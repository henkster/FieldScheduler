using System;
using System.Collections.Generic;

namespace Domain
{
    public class Field : DomainObject<int>
    {
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
    }
}