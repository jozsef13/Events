using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Models.Entities
{
    public class DetailsViewModel
    {
        public Event DetailedEvent { get; set; }
        public List<Event> SimilarEvents { get; set; }
    }
}
