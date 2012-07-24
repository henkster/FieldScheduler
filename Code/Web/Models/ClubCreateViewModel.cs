using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ClubCreateViewModel
    {
        public string Activity { get; set; }
        public string Size { get; set; }
        public string Date { get; set; }
        public int SlotId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CityState { get; set; }

        public static ClubCreateViewModel LoadFromSelect(string activity, string size, string date, int slotId)
        {
            var vm = new ClubCreateViewModel
                         {
                             Activity = activity,
                             Size = size,
                             Date = date,
                             SlotId = slotId
                         };

            return vm;
        }
    }
}