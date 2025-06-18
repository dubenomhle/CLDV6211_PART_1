namespace EventEase_Part_1.Models
{
   public class EventType
    {
        public int? EventTypeID { get; set; }
        public string Name { get; set; }

        public ICollection<Event> Event { get; set; }
    }

}

