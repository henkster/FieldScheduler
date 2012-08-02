using System.Collections.Generic;

namespace Domain
{
    public class User : DomainObject<int>
    {
        public User()
        {
            Teams = new List<ClubTeam>();
        }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public Roles Roles
        {
            get { return (Roles) RolesAsInt; }
            set { RolesAsInt = (int) value; }
        }

        public int RolesAsInt { get; set; }

        public virtual List<ClubTeam> Teams { get; private set; }

        public bool IsIn(Roles role)
        {
            return (Roles & role) == role;
        }
    }
}