using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Models.Entities
{
    public class EventsViewModel
    {
        public List<Event> EventsList { get; set; }
        public List<EventType> EventTypes { get; set; }
        public List<EventSubType> EventSubTypes { get; set; }
    }
}
