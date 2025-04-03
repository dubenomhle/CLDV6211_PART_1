namespace EventEase_Part_1.Models
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string ImageUrl { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
    }

}
