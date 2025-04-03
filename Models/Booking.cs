namespace EventEase_Part_1.Models

{
    public class Booking
    {
        public int BookingID { get; set; }
        public int EventID { get; set; }
        public int VenueID { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;

        public Event Event { get; set; }
        public Venue Venue { get; set; }
    }

}
