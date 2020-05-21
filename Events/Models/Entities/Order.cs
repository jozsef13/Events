using Events.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class Order
    {
        public enum Status
        {
            Placed,
            Approved,
            Delivered
        }

        public Guid Id { get; set; }
        public Status OrderStatus { get; set; }
        public int TicketsQuantity { get; set; }

        public virtual Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual string UserName { get; set; }
        public virtual EventsUser User { get; set; }
    }
}