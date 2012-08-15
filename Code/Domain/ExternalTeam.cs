using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ExternalTeam : Team
    {
        [Required]
        public string ContactName { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }

        [Required]
        public string ContactEmailAddress { get; set; }

        [Required]
        public string CityState { get; set; }

        public override string FullName
        {
            get
            {
                return string.Format("{0} - {1}{2}",
                                     Club.Name,
                                     Name,
                                     Level != Level.Academy && Level != Level.Other
                                         ? string.Format(" ({0})", Level)
                                         : null);
            }

        }

        public override System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Club == null) yield return new ValidationResult("Club must be created/selected for this team.");

            foreach (ValidationResult result in base.Validate(validationContext)) yield return result;
        }
    }
}