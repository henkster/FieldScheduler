using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class Team : DomainObject<int>
    {
        [Required]
        public string Name { get; set; }
        
        public virtual Club Club { get; set; }
        
        public virtual Division Division { get; set; }

        public Level Level
        {
            get { return (Level) LevelAsInt; }
            set { LevelAsInt = (int) value; }
        }

        public int LevelAsInt { get; set; }

        public abstract string FullName { get; }
    }
}