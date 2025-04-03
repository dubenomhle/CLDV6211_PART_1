namespace EventEase_Part_1.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? VenueId { get; set; }
        public Venue Venue { get; set; }
    }
}

