using System.Collections.Generic;
using System.Web.Mvc;
using Domain;

namespace Web.Models.GameScheduleModels
{
    public class SelectionViewModel
    {
        public SelectionViewModel(IList<Team> teams, IList<Division> divisions, Division selectedDivision)
        {
            Team1DivisionList = new List<SelectListItem>();

            foreach (Division division in divisions)
            {
                var item = new SelectListItem { Text = division.Name, Value = division.Id.ToString(), Selected = division == selectedDivision };
                Team1DivisionList.Add(item);
            }

            Team2DivisionList = new List<SelectListItem>();

            foreach (Division division in divisions)
            {
                var item = new SelectListItem { Text = division.Name, Value = division.Id.ToString(), Selected = division == selectedDivision };
                Team2DivisionList.Add(item);
            }

            Team1List = new List<SelectListItem>();
            
            foreach (Team team in teams)
            {
                Team1List.Add(new SelectListItem { Text = team.FullName, Value = team.Id.ToString() });
            }

            Team2List = new List<SelectListItem>();

            foreach (Team team in teams)
            {
                Team2List.Add(new SelectListItem { Text = team.FullName, Value = team.Id.ToString() });
            }
        }
        public int SlotId { get; set; }

        public string Activity { get; set; }

        public string Size { get; set; }

        public string Date { get; set; }

        public string DateFormatted
        {
            get { return string.Format("{0}/{1}/{2}", Date.Substring(2, 2), Date.Substring(0, 2), Date.Substring(4, 4)); }
        }

        public string Description { get; set; }

        public int Team1 { get; set; }
        public int Team2 { get; set; }

        public int Team1Division { get; set; }
        public int Team2Division { get; set; }

        public List<SelectListItem> Team1DivisionList { get; set; }
        public List<SelectListItem> Team2DivisionList { get; set; }
        public List<SelectListItem> Team1List { get; set; }
        public List<SelectListItem> Team2List { get; set; }

        public string Notes { get; set; }
    }
}