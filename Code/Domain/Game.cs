using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Game : DomainObject<int>
    {
        [Required]
        public virtual Slot Slot { get; set; }
        
        [Required]
        public virtual Team Team1 { get; set; }

        public virtual Team Team2 { get; set; }
        
        [Required]
        public bool AreRefereesNeed { get; set; }

        public bool AreRefereesConfirmed { get; set; }
        
        public string Notes { get; set; }
        
        [Required]
        public virtual User ScheduledBy { get; set; }
        
        [Required]
        public DateTime ScheduledOn { get; set; }
        
        public virtual User CanceledBy { get; set; }
        public DateTime? CanceledOn { get; set; }

        public bool IsCanceled { get { return CanceledOn.HasValue; } }

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