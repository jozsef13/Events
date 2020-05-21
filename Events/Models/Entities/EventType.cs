using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class EventType
    {
        public Guid Id { get; set; }
        public TypeEnum Name { get; set; }

        public virtual ICollection<EventSubType> SubTypes { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }

    public enum TypeEnum
    {
        Concerts,
        Cultural,
        Sports,
        Family,
        Others
    }
}