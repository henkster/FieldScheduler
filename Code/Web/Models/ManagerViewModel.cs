using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Web.Models
{
    public class ManagerViewModel
    {
        public string Age { get; set; }
        public string Gender { get; set; }
        public string TeamName { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public static IEnumerable<ManagerViewModel> LoadList(IEnumerable<ExternalTeam> teams)
        {
            foreach (ExternalTeam team in teams)
            {
                yield return new ManagerViewModel
                                 {
                                     Age = string.Format("U{0}", team.Division.Age),
                                     Gender = team.Division.Gender,
                                     TeamName = team.Name,
                                     ManagerName = team.ContactName,
                                     PhoneNumber = team.ContactPhoneNumber,
                                     EmailAddress = team.ContactEmailAddress
                                 };
            }
        }

        public static IEnumerable<ManagerViewModel> LoadList(IEnumerable<ClubTeam> teams)
        {
            foreach (ClubTeam team in teams)
            {
                foreach (User manager in team.Managers.Where(m => m.IsActive).OrderBy(m => m.Name))
                {
                    yield return new ManagerViewModel
                                     {
                                         Age = string.Format("U{0}", team.Division.Age),
                                         Gender = team.Division.Gender,
                                         TeamName = team.Name,
                                         ManagerName = manager.Name,
                                         PhoneNumber = manager.PhoneNumber,
                                         EmailAddress = manager.EmailAddress
                                     };
                }
            }
        }
    }
}