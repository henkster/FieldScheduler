using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Club : DomainObject<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CityState { get; set; }
    }
}