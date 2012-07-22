using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class SettingEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        
        [HiddenInput(DisplayValue = true)]
        public string Key { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string Value { get; set; }

        public static SettingEditViewModel Load(Setting setting)
        {
            return Mapper.Map<Setting, SettingEditViewModel>(setting);
        }
    }
}