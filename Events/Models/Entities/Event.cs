using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateTime { get; set; }
        public int MaximumTickets { get; set; }
        public int AvailableTickets { get; set; }
        public string CoverPhotoPath { get; set; }

        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; }
        public Guid ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public Guid TypeId { get; set; }
        public virtual EventType Type { get; set; }
        public Guid SubTypeId { get; set; }
        public virtual EventSubType SubType { get; set; }
    }
}