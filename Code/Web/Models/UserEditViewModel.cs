using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {
            IsActive = true;
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required, Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public bool Admin { get; set; }
        public bool Manager { get; set; }
        public bool Referee { get; set; }
        public bool Reader { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required, StringLength(100, MinimumLength = 6, ErrorMessage = "{0} must be at least 6 characters.")]
        public string Password { get; set; }

        public static UserEditViewModel Load(User user)
        {
            return Mapper.Map<User, UserEditViewModel>(user);
        } 
    }
}