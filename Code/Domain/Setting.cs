namespace Domain
{
    public class Setting : DomainObject<int>
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}