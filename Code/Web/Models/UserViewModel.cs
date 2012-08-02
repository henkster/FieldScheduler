using System.Collections.Generic;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class UserViewModel
    {
        public int Id;
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Roles { get; set; }
        
        public static IEnumerable<UserViewModel> LoadList(IEnumerable<User> users)
        {
            foreach (User user in users) yield return Load(user);
        }

        public static UserViewModel Load(User user)
        {
            return Mapper.Map<User, UserViewModel>(user);
        }
    }
}