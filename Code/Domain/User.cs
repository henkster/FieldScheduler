using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User : DomainObject<int>
    {
        public User()
        {
            Teams = new List<ClubTeam>();
        }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string EmailAddress { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        
        public bool IsActive { get; set; }

        public Roles Roles
        {
            get { return (Roles)RolesAsInt; }
            set { RolesAsInt = (int)value; }
        }
        
        public int RolesAsInt { get; set; }

        public virtual List<ClubTeam> Teams { get; private set; }
       
        public bool IsIn(Roles role)
        {
            return (Roles & role) == role;
        }

        public bool IsInAny(params Roles[] roles)
        {
            foreach (Roles role in roles) if (IsIn(role)) return true;

            return false;
        }
    }
}