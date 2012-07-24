using System.Collections.Generic;
using Domain;

namespace Web.Models.AccountModels
{
    public class AccountSummaryViewModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Teams { get; set; } 

        public static AccountSummaryViewModel Load(User user)
        {
            var vm = new AccountSummaryViewModel();

            vm.Name = user.Name;
            vm.EmailAddress = user.EmailAddress;
            vm.PhoneNumber = user.PhoneNumber;

            vm.Teams = new List<string>();

            foreach (ClubTeam team in user.Teams)
            {
                vm.Teams.Add(team.Name);
            }

            return vm;
        }
    }
}