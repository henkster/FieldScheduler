using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using Domain;

namespace Web.Models
{
    public class SlotListViewModel
    {
        public IEnumerable<SlotViewModel> Slots { get; set; }
        public SelectList FieldList { get; set; }
        public int FieldId { get; set; }

        public static void InitializeList(SlotListViewModel vm, DbSet<Field> fields)
        {
            var items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "ALL", Value = "0"});

            foreach (Field field in fields)
            {
                items.Add(new SelectListItem { Text = field.Description, Value = field.Id.ToString() });
            }

            vm.FieldList = new SelectList(items, "Value", "Text");
        }
    }
}