using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Web.Models
{
    public class FieldCreateViewModel : IValidatableObject
    {
        [Required]
        public string Description { get; set; } // TODO finish this and make sure we figure out enums and dropdowns
        public bool HasLights { get; set; }
        public bool AreRefereesRequired { get; set; }
        public bool AllowTraining { get; set; }
        public bool AllowFriendly { get; set; }
        public bool AllowStateLeague { get; set; }
        public FieldSize Size { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!AllowFriendly && !AllowStateLeague && !AllowTraining) yield return new ValidationResult("At least one activity must be selected.");
        }
    }
}