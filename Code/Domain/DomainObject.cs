using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class DomainObject<T> : IValidatableObject
    {
        public T Id { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}