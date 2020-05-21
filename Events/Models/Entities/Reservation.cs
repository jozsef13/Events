using Events.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public int TicketsQuantity { get; set; }

        public virtual Guid TicketId { get; set; }
        public virtual Ticket TicketReserved { get; set; }
        public virtual string UserName { get; set; }
        public virtual EventsUser User { get; set; }
    }
}