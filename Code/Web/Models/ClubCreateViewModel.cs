using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ClubCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CityState { get; set; }

        public string ReturnTo { get; set; }
    }
}