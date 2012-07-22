using System.Web.Mvc;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class UserEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static UserEditViewModel Load(User user)
        {
            return Mapper.Map<User, UserEditViewModel>(user);
        } 
    }
}