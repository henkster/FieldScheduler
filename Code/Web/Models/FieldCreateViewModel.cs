using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
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

        public List<FieldConflictViewModel> Conflicts { get; set; } 
    }

    public class FieldConflictViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsConflict { get; set; }

        public static List<FieldConflictViewModel> LoadList(DbSet<Field> fields)
        {
            var list = new List<FieldConflictViewModel>();

            foreach (Field field in fields.OrderBy(f => f.Description))
            {
                list.Add(new FieldConflictViewModel
                             {
                                 Id = field.Id,
                                 Description = field.Description
                             });
            }

            return list;
        }
    }
}