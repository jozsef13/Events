using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(EventsDbContext eventsDbContext) : base(eventsDbContext)
        { }

        public List<Reservation> GetUsersReservations(string id)
        {
            List<Reservation> reservations = context.Reservations.Where(r => r.User.Id == id).ToList();

            foreach(var reservation in reservations)
            {
                var ticket = context.Tickets.Find(reservation.TicketId);
                if(ticket == null)
                {
                    throw new Exception("Ticket not found!");
                }
                var iEvent = context.Events.Find(ticket.EventId);
                if (iEvent == null)
                {
                    throw new Exception("Event not found!");
                }
                var artist = context.Artists.Find(iEvent.ArtistId);
                if (artist == null)
                {
                    throw new Exception("Artist not found!");
                }
                iEvent.Artist = artist;
                ticket.TicketEvent = iEvent;
                reservation.TicketReserved = ticket;
            }

            return reservations;
        }
    }
}
