using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Game : DomainObject<int>
    {
        [Required]
        public Slot Slot { get; set; }
        
        [Required]
        public Team Team1 { get; set; }

        public Team Team2 { get; set; }
        
        [Required]
        public bool AreRefereesNeed { get; set; }

        public bool AreRefereesConfirmed { get; set; }
        
        public string Notes { get; set; }
        
        [Required]
        public User ScheduledBy { get; set; }
        
        [Required]
        public DateTime ScheduledOn { get; set; }
        
        public User CanceledBy { get; set; }
        public DateTime? CanceledOn { get; set; }

        public Activities Activity
        {
            get { return (Activities)Enum.ToObject(typeof(Activities), ActivityAsInt); }
            set { ActivityAsInt = (int)value; }
        }

        public int ActivityAsInt { get; set; }

        public override System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CanceledBy != null && !CanceledOn.HasValue) yield return new ValidationResult("Canceled On must be defined when Canceled By is defined.");

            if (Activity != Activities.Training && Team2 == null) yield return new ValidationResult("Both teams must be defined for friendlies and State League games.");
        }
    }
}