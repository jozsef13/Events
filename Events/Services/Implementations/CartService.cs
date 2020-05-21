using Events.Areas.Identity.Data;
using Events.Models;
using Events.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class CartService : ICartService
    {
        private ICartRepository cartRepository;
        private ITicketRepository ticketRepository;

        public CartService(ICartRepository cartRepository, ITicketRepository ticketRepository)
        {
            this.cartRepository = cartRepository;
            this.ticketRepository = ticketRepository;
        }

        public void AddOne(Cart oldCart)
        {
            oldCart.TicketsQuantity++;
            oldCart.TotalPrice += oldCart.CartTicket.Price;
            oldCart.CartTicket.Quantity++;
            cartRepository.Update(oldCart);
        }

        public Cart Create(EventsUser user, Ticket ticket, int ticketsQuantity)
        {
            
            Ticket cartTicket = ticket;
            if (cartTicket == null)
            {
                throw new Exception("Cart is not valid!");
            }

            if (ticketsQuantity == 0)
            {
                throw new Exception("Quantity is not valid!");
            }
            cartTicket.Quantity = ticketsQuantity;
            Cart cart = new Cart { Id = Guid.NewGuid(), CartTicket = cartTicket, TicketsQuantity = cartTicket.Quantity, TotalPrice = cartTicket.Price * cartTicket.Quantity, User = user};
            user.Cart = cart;
            cartRepository.Add(cart);
            return cart;
        }

        public Cart DeleteTickets(Cart oldCart, int ticketsQuantity)
        {
            if(oldCart.TicketsQuantity == ticketsQuantity)
            {
                oldCart.CartTicket.Quantity = 0;
                cartRepository.Delete(oldCart);
                return null;
            }
            else
            {
                oldCart.TicketsQuantity -= ticketsQuantity;
                oldCart.TotalPrice -= oldCart.CartTicket.Price * ticketsQuantity;
                oldCart.CartTicket.Quantity -= ticketsQuantity;
                cartRepository.Update(oldCart);
                return oldCart;
            }
        }

        public Cart GetCartById(string userId)
        {
            return cartRepository.GetByForeignUserId(userId);
        }

        public void Update(Cart oldCart, int ticketsQuantity)
        {
            oldCart.TicketsQuantity += ticketsQuantity;
            oldCart.TotalPrice = oldCart.CartTicket.Price * oldCart.TicketsQuantity;
            oldCart.CartTicket.Quantity += ticketsQuantity;
            cartRepository.Update(oldCart);
        }
    }
}
