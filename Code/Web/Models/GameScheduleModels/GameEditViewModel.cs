using Domain;

namespace Web.Models.GameScheduleModels
{
    public class GameEditViewModel
    {
        public string Activity { get; set; }
        public string Size { get; set; }
        public string DateFormatted { get; set; }
        public string Description { get; set; }
        public int Team1 { get; set; }
        public int Team2 { get; set; }
        public string Notes { get; set; }

        public int Team1Division { get; set; }
        public int Team2Division { get; set; }

        public static GameEditViewModel Load(Game game)
        {
            var vm = new GameEditViewModel
                         {
                             Description = CreateDescription(game),
                             Team1 = game.Team1.Id,
                             Team2 = game.Team2.Id
                         };

            return vm;
        }

        private static string CreateDescription(Game game)
        {
            return string.Format("{0}, {1} - {2}-{3} - {4} - {5}",
                                 game.Slot.StartDateTime.DayOfWeek,
                                 game.Slot.StartDateTime.ToShortDateString(),
                                 game.Slot.StartDateTime.ToShortTimeString(),
                                 game.Slot.EndDateTime.ToShortTimeString(),
                                 game.Slot.Field.Size,
                                 game.Slot.Field.Description);
        }
    }
}