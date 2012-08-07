using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using Domain;

namespace Web.Models
{
    public class TeamCreateViewModel
    {
        public int ClubId { get; set; }
        public List<SelectListItem> ClubList { get; set; }
        public int DivisionId { get; set; }
        public List<SelectListItem> DivisionList { get; set; }
        public string ReturnTo { get; set; }

        [Required, Display(Name = "Team Name")]
        public string NewTeamName { get; set; }

        [Required, Display(Name = "City, State")]
        public string CityState { get; set; }

        [Required, Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        
        [Required, Email, Display(Name = "Contact Email Address")]
        public string ContactEmailAddress { get; set; }

        [Required, Display(Name = "Contact Phone Number")]
        public string ContactPhoneNumber { get; set; }

        [Required]
        public Level Level { get; set; }

        public static TeamCreateViewModel LoadFromSelect(List<Club> clubs, List<Division> divisions)
        {
            var vm = new TeamCreateViewModel
                         {
                             ClubList = new List<SelectListItem>(),
                             DivisionList = new List<SelectListItem>()
                         };

            foreach (Club club in clubs)
            {
                vm.ClubList.Add(new SelectListItem {Text= club.Name, Value = club.Id.ToString()});
            }

            foreach (Division division in divisions)
            {
                vm.DivisionList.Add(new SelectListItem { Text = division.Name, Value = division.Id.ToString() });
            }

            return vm;
        }
    }
}