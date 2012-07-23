namespace Web.Models.GameScheduleModels
{
    public class GameSizeSelectViewModel
    {
        public string Activity { get; set; }

        public GameSizeSelectViewModel(string activity)
        {
            Activity = activity;
        }
    }
}