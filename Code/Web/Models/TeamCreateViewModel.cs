using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;

namespace Web.Models
{
    public class TeamCreateViewModel
    {
        public string Activity { get; set; }
        public string Size { get; set; }
        public string Date { get; set; }
        public int SlotId { get; set; }
        public int ClubId { get; set; }
        public List<SelectListItem> ClubList { get; set; }
        public int DivisionId { get; set; }
        public List<SelectListItem> DivisionList { get; set; }

        [Required]
        public string NewTeamName { get; set; }

        [Required]
        public string CityState { get; set; }

        [Required]
        public string ContactName { get; set; }
        
        [Required]
        public string ContactEmailAddress { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }

        public static TeamCreateViewModel LoadFromSelect(string activity, string size, string date, int slotId, List<Club> clubs, List<Division> divisions)
        {
            var vm = new TeamCreateViewModel
                         {
                             Activity = activity,
                             Size = size,
                             Date = date,
                             SlotId = slotId,
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