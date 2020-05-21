using Events.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Models.Entities
{
    public class ReservationViewModel
    {
        public List<Reservation> Reservations { get; set; }
        public EventsUser User { get; set; }
    }
}
