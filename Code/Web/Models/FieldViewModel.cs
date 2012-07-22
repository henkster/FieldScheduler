using System.Collections.Generic;
using AutoMapper;
using Domain;

namespace Web.Models
{
    public class FieldViewModel
    {
        public string Description { get; set; }
        public bool AreRefereesRequired { get; set; }
        public bool HasLights { get; set; }
        public FieldSize Size { get; set; }

        public string SizeFormatted
        {
            get
            {
                switch (Size)
                {
                    case FieldSize.ElevenVsEleven:
                        return "11v11";
                    case FieldSize.EightVsEight:
                        return "8v8";
                    case FieldSize.SixVsSix:
                        return "6v6";
                }
                return "Unknown";
            }
        }

        public static IEnumerable<FieldViewModel> LoadList(IEnumerable<Field> fields)
        {
            foreach (Field field in fields) yield return Load(field);
        }

        public static FieldViewModel Load(Field field)
        {
            return Mapper.Map<Field, FieldViewModel>(field);
        }
    }
}