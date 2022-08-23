namespace BitBuddy.Core.Entities
{
    public class Interest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserInterest>? UserInterests { get; set; }
        public ICollection<EventInterest> EventInterests { get; set; }

        public override bool Equals(object o)
        {
            var other = o as Interest;
            return other?.Name == Name;
        }

        // Note: this is important so the select can compare 
        public override int GetHashCode() => Name.GetHashCode();


    }
}
