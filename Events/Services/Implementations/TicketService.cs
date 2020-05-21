using Events.Models;
using Events.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class TicketService : ITicketService
    {
        private ITicketRepository ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public Ticket Create(Ticket ticket)
        {
            if(ticket == null)
            {
                throw new Exception("The object is not valid");
            }
            return ticketRepository.Add(ticket);
        }

        public Ticket GetTicketById(Guid ticketId)
        {
            return ticketRepository.GetById(ticketId);
        }
    }
}
