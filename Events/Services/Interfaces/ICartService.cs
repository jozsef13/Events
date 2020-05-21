using Events.Areas.Identity.Data;
using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface ICartService
    {
        Cart Create(EventsUser user, Ticket ticket, int ticketsQuantity);
        Cart GetCartById(string userId);
        void Update(Cart oldCart, int ticketsQuantity);
        Cart DeleteTickets(Cart oldCart, int ticketsQuantity);
        void AddOne(Cart oldCart);
    }
}
