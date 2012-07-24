using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;

namespace Web.Models
{
    public class SlotCreateViewModel : IValidatableObject
    {
        [Required]
        public int FieldId { get; set; }

        [Required, Display(Name = "Day of Week"), RegularExpression("^Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday$", ErrorMessage = "{0} must be Monday, Tuesday, etc.")]
        public DayOfWeek? DayOfWeek { get; set; }

        [Required, Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required, Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required, Display(Name = "Start Time")]
        public DateTime? StartTime { get; set; }

        [Required, Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        [Required, Display(Name = "Minutes per Slot"), RegularExpression("^75|90|120$", ErrorMessage = "{0} must be 75, 90, or 120")]
        public int? MinutesPerSlot { get; set; }

        [Display(Name = "It Is An Error If Times Don't Line Up With Existing Slots")]
        public bool IsErrorIfTimeMismatch { get; set; }

        [Display(Name = "It Is An Error If Slots Already Exist In These Times")]
        public bool IsErrorIfExistingSlots { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.Value.Date < StartDate.Value.Date) yield return new ValidationResult("End Date cannot be before Start.");
            if (EndTime.Value.TimeOfDay < StartTime.Value.AddMinutes(MinutesPerSlot.Value).TimeOfDay)
                yield return new ValidationResult("End Time cannot be before Start (plus minutes for slot).");
            
        }

        public SelectList FieldList { get; set; }
        public SelectList MinutesList { get; set; }

        public static void InitializeLists(SlotCreateViewModel vm, IEnumerable<Field> fields)
        {
            vm.FieldList = new SelectList(fields, "Id", "Description");

            var minutes = new List<SelectListItem>();
            foreach (var m in Enum.GetValues(typeof(SlotDuration)))
            {
                minutes.Add(new SelectListItem { Text = ((int)m).ToString(), Value = ((int)m).ToString()});
            }
            vm.MinutesList = new SelectList(minutes, "Value", "Text");
        }
    }
}