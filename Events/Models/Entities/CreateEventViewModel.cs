using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Models.Entities
{
    public class CreateEventViewModel
    {
        public List<Artist> Artists { get; set; }
        public List<Location> Locations { get; set; }
        public List<EventType> EventTypes { get; set; }
        public List<EventSubType> EventSubTypes { get; set; }
        public Event Event { get; set; }
        public Ticket Ticket { get; set; }
    }
}
