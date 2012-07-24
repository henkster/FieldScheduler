﻿using System.Collections.Generic;

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
        public bool IsAdmin { get; set; }

        public virtual List<ClubTeam> Teams { get; private set; }
    }
}