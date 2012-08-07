using System.ComponentModel.DataAnnotations;

namespace Web.Models.AccountModels
{
    public class AccountEditViewModel
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}