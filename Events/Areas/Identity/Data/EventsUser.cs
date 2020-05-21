using Events.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Areas.Identity.Data
{
    public class EventsUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPhoto { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GenderType { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> TikcketsOrdered { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Reservation> EventsReservations { get; set; }

    }
}
