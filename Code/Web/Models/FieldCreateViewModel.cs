using Domain;

namespace Web.Models
{
    public class FieldCreateViewModel
    {
        public string Description { get; set; } // TODO finish this and make sure we figure out enums and dropdowns
        public bool HasLights { get; set; }
        public bool AreRefereesRequired { get; set; }
        public Activities AllowedActivities { get; set; }
    }
}