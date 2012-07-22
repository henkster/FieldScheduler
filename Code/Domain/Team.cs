namespace Domain
{
    public abstract class Team : DomainObject<int>
    {
        public string Name { get; set; }
        public Club Club { get; set; }
    }
}