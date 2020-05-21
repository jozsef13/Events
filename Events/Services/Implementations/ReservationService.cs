using Events.Areas.Identity.Data;
using Events.Models;
using Events.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class ReservationService : IReservationService
    {
        private IEventRepository eventRepository;
        private IReservationRepository reservationRepository;
        private ITicketRepository ticketRepository;

        public ReservationService(IEventRepository eventRepository, IReservationRepository reservationRepository, ITicketRepository ticketRepository)
        {
            this.eventRepository = eventRepository;
            this.reservationRepository = reservationRepository;
            this.ticketRepository = ticketRepository;
        }

        public Reservation Create(EventsUser user, Ticket ticket, int ticketsQuantity)
        {
            var iEvent = eventRepository.GetById(ticket.EventId);
            if(iEvent == null)
            {
                throw new Exception("Event not found!");
            }
            ticket.TicketEvent = iEvent;
            Reservation reservation = new Reservation { ReservationId = Guid.NewGuid(), TicketReserved = ticket, TicketsQuantity = ticketsQuantity, User = user };
            reservation.TicketReserved.TicketEvent.AvailableTickets -= ticketsQuantity;
            reservationRepository.Add(reservation);
            return reservation;
        }

        public void Delete(Reservation reservation)
        {
            reservation.TicketReserved.TicketEvent.AvailableTickets += reservation.TicketsQuantity;
            reservationRepository.Delete(reservation);
        }

        public Reservation GetById(Guid id, EventsUser user)
        {
            var reservation = reservationRepository.GetById(id);
            reservation.User = user;
            var ticket = ticketRepository.GetById(reservation.TicketId);
            if(ticket == null)
            {
                throw new Exception("Ticket not found!");
            }

            var iEvent = eventRepository.GetById(ticket.EventId);
            if(iEvent == null)
            {
                throw new Exception("Event not found!");
            }
            ticket.TicketEvent = iEvent;
            reservation.TicketReserved = ticket;
            return reservation;
        }

        public List<Reservation> GetUsersReservations(string id)
        {
            return reservationRepository.GetUsersReservations(id);
        }
    }
}
