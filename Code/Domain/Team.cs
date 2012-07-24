namespace Domain
{
    public abstract class Team : DomainObject<int>
    {
        public string Name { get; set; }
        public virtual Club Club { get; set; }
        public virtual Division Division { get; set; }

        public string FullName
        {
            get { return string.Format("{0} - {1}", Club.Name, Name); }
        }
    }
}