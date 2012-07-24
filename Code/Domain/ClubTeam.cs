using System.Collections.Generic;

namespace Domain
{
    public class ClubTeam : Team
    {
        public ClubTeam()
        {
            Managers = new List<User>();
        }
        public virtual List<User> Managers { get; private set; }
    }
}