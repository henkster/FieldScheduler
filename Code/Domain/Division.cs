namespace Domain
{
    public class Division : DomainObject<int>
    {
        public int Age { get; set; }
        public string Gender { get; set; }

        public Division(int age, string gender)
        {
            Age = age;
            Gender = gender;
        }
        public Division()
        {
        }

        public string Name { get { return string.Format("U{0} {1}", Age, Gender); } }
    }
}