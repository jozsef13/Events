using Events.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Models.Entities
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public EventsUser User { get; set; }
    }
}
