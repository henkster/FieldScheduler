using Domain;

namespace Web.Models
{
    public class GameViewModel
    {
        public Slot Slot { get; set; }
        public Activities Activity { get; set; }

        public string Reason
        {
            get
            {
                switch (Activity)
                {
                    case Activities.Friendly:
                        return "Friendly";
                    case Activities.StateLeague:
                        return "State League";
                    case Activities.Training:
                        return "Training";
                }
                return "Unknown";
            }
        } 
    }
}