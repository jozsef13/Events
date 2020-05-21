using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Guid EventId { get; set; }
        public virtual Event TicketEvent { get; set; }

    }
}