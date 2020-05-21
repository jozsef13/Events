using Events.Areas.Identity.Data;
using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface IReservationService
    {
        Reservation Create(EventsUser user, Ticket ticket, int ticketsQuantity);
        List<Reservation> GetUsersReservations(string id);
        Reservation GetById(Guid id, EventsUser user);
        void Delete(Reservation reservation);
    }
}
