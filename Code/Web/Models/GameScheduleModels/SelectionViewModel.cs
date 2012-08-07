using System.Collections.Generic;
using System.Web.Mvc;
using Domain;

namespace Web.Models.GameScheduleModels
{
    public class SelectionViewModel
    {
        private bool _areRefereesNeeded;
        
        public SelectionViewModel(IList<Team> teams1, IList<Team> teams2, IList<Division> divisions, Team team1 = null, Team team2 = null, Division division1 = null, Division division2 = null)
        {
            Team1List = new SelectList(teams1, "Id", "FullName", team1 != null ? team1.Id.ToString() : string.Empty);
            Team2List = new SelectList(teams2, "Id", "FullName", team2 != null ? team2.Id.ToString() : string.Empty);
            Division1List = new SelectList(divisions, "Id", "Name", division1 != null ? division1.Id.ToString() : string.Empty);
            Division2List = new SelectList(divisions, "Id", "Name", division2 != null ? division2.Id.ToString() : string.Empty);
        }
        public SelectionViewModel() {}

        public int Id { get; set; }

        public int SlotId { get; set; }

        public string Activity { get; set; }

        public string Size { get; set; }

        public string Date { get; set; }

        public string DateFormatted
        {
            get { return Date.Contains("/") ? Date : string.Format("{1}/{0}/{2}", Date.Substring(2, 2), Date.Substring(0, 2), Date.Substring(6, 2)); }
        }

        public string Description { get; set; }

        public int Team1Id { get; set; }
        public int Team2Id { get; set; }

        public int Division1Id { get; set; }
        public int Division2Id { get; set; }

        public SelectList Division1List { get; set; }
        public SelectList Division2List { get; set; }
        public SelectList Team1List { get; set; }
        public SelectList Team2List { get; set; }

        public string Notes { get; set; }

        public bool AreRefereesNeeded
        {
            get
            {
                if (AreRefereesRequired && !_areRefereesNeeded) _areRefereesNeeded = true;

                return _areRefereesNeeded;
            }
            set { _areRefereesNeeded = AreRefereesRequired || value; }
        }

        public bool AreRefereesRequired { get; set; }
    }
}