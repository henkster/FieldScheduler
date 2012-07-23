using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Web.Models.GameScheduleModels
{
    public class SlotSummaryViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string ScheduledByName { get; set; }

        public static SlotSummaryViewModel Load(Slot slot)
        {
            string description = CreateDescription(slot);

            bool isAvailable = slot.IsAvailable;

            string scheduledByName = !isAvailable ? slot.Games.Single(g => !g.IsCanceled).ScheduledBy.Name : string.Empty;

            var viewModel = new SlotSummaryViewModel
                                {
                                    Id = slot.Id,
                                    Description = description,
                                    IsAvailable = isAvailable,
                                    ScheduledByName = scheduledByName
                                };

            return viewModel;
        }

        public static string CreateDescription(Slot slot)
        {
            return string.Format("{0} - {1}-{2}",
                                 slot.Field.Description,
                                 slot.StartDateTime.ToShortTimeString(),
                                 slot.EndDateTime.ToShortTimeString());
        }

        public static IEnumerable<SlotSummaryViewModel> LoadList(List<Slot> slots)
        {
            foreach (Slot slot in slots) yield return Load(slot);
        }
    }
}