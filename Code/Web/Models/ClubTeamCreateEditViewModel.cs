using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain;

namespace Web.Models
{
    public class ClubTeamCreateEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int DivisionId { get; set; }
        public Level Level { get; set; }

        public SelectList DivisionList { get; set; }

        public static void InitializeList(ClubTeamCreateEditViewModel vm, IEnumerable<Division> divisions)
        {
            var orderedDivisions = new List<Division>(divisions).OrderBy(d => d.Age).ThenBy(d => d.Gender);

            vm.DivisionList = new SelectList(orderedDivisions, "Id", "Name");
        }

        public static ClubTeamCreateEditViewModel Load(ClubTeam team, IEnumerable<Division> divisions)
        {
            var vm = new ClubTeamCreateEditViewModel
                         {
                             DivisionId = team.Division.Id,
                             Id = team.Id,
                             Level = team.Level,
                             Name = team.Name
                         };

            InitializeList(vm, divisions);

            return vm;
        }
    }
}