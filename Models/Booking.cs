namespace EventEase_Part_1.Models;


public class Booking
{
    public int BookingId { get; set; }
    public int EventId { get; set; }
    public int VenueId { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.Now;

    public Event Event { get; set; }
    public Venue Venue { get; set; }
}
