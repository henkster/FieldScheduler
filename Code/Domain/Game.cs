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

        private bool _areRefereesNeeded;

        [Required]
        public bool AreRefereesNeeded
        {
            get
            {
                if (Slot != null && Slot.Field.AreRefereesRequired && !_areRefereesNeeded) _areRefereesNeeded = true;

                return _areRefereesNeeded;
            }
            set { _areRefereesNeeded = (Slot != null && Slot.Field.AreRefereesRequired) || value; } // TODO Crazy line for DbInitializer
        }

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

        public int Age
        {
            get
            {
                if (Team1 != null && Team2 != null) return Math.Max(Team1.Division.Age, Team2.Division.Age);

                if (Team1 == null && Team2 != null) return Team2.Division.Age;

                if (Team1 != null && Team2 == null) return Team1.Division.Age;

                return 0;
            }
        }

        public string Gender
        {
            get
            {
                if (Team1 != null && Team2 != null)
                {
                    if (Team1.Division.Gender == Team2.Division.Gender) return Team1.Division.Gender.ToLower();

                    return "coed";
                }

                if (Team1 == null && Team2 != null) return Team2.Division.Gender.ToLower();

                if (Team1 != null && Team2 == null) return Team1.Division.Gender.ToLower();

                return string.Empty;
            }
        }

        public Level Level
        {
            get
            {
                if (Team1 != null && Team2 != null)
                {
                    if (Team1.Level == Level.Other || Team2.Level == Level.Other) return Level.Other;
                    if (Team1.Level == Level.Academy || Team2.Level == Level.Academy) return Level.Academy;
                    if (Team1.Level == Level.D3 || Team2.Level == Level.D3) return Level.D3;
                    if (Team1.Level == Level.D2 || Team2.Level == Level.D2) return Level.D2;
                    return Level.D1;
                }
                if (Team1 != null) return Team1.Level;
                if (Team2 != null) return Team2.Level;
                return Level.Other;
            }
        }

        public override System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CanceledBy != null && !CanceledOn.HasValue) yield return new ValidationResult("Canceled On must be defined when Canceled By is defined.");

            if (Activity != Activities.Training && Team2 == null) yield return new ValidationResult("Both teams must be defined for friendlies and State League games.");
        }
    }
}