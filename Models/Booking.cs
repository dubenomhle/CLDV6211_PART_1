using System.ComponentModel.DataAnnotations;

namespace EventEase_Part_1.Models

{
    public class Booking
    {
        public int BookingID { get; set; }

        [Required(ErrorMessage = "The Venue field is required.")]
        public int? VenueID { get; set; }

        [Required(ErrorMessage = "The Event field is required.")]
        public int? EventID { get; set; }

        public DateTime BookingDate { get; set; }

        public Venue Venue { get; set; }
        public Event Event { get; set; }
    }


}

