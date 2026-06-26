namespace Domain.Entities
{
    public class Venue
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string Location { get;  set; }
        public int Capacity { get;  set; }
        public List<Event> Events { get; private set; } = new();
        public Venue() { } // EF necesita constructor vacío

        public Venue(string name, int capacity, string location)
        {
            Id = Guid.NewGuid();
            Name = name;
            Capacity = capacity;
            Location = location;
        }
    }
}
