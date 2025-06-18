namespace EventEase_Part_1.ViewModels
{
    public class BookingViewModel
    {
        public int BookingID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventType { get; set; }
        public string VenueName { get; set; }
        public string VenueLocation { get; set; }
        public bool VenueAvailable { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
