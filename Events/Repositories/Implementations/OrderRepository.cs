using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(EventsDbContext context) : base(context)
        { }

        public List<Order> GetUsersOrders(string id)
        {
            List<Order> orders = context.Orders.Where(o => o.User.Id == id).ToList();
            foreach(var order in orders)
            {
                var ticket = context.Tickets.Find(order.TicketId);
                if(ticket == null)
                {
                    throw new Exception("Ticket not found!");
                }
                var iEvent = context.Events.Find(ticket.EventId);
                if(iEvent == null)
                {
                    throw new Exception("Event not found!");
                }
                var artist = context.Artists.Find(iEvent.ArtistId);
                if(artist == null)
                {
                    throw new Exception("Artist not found!");
                }
                iEvent.Artist = artist;
                ticket.TicketEvent = iEvent;
                order.Ticket = ticket;
            }

            return orders;
        }
    }
}
