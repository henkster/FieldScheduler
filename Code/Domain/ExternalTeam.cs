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
    }
}