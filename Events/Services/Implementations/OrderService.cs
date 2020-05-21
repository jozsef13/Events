using Events.Models;
using Events.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository orderRepository;
        private ICartRepository cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
        }

        public Order Create(Cart cart)
        {
            Order order = new Order { Id = Guid.NewGuid(), OrderStatus = Order.Status.Placed, Ticket = cart.CartTicket, TicketsQuantity = cart.TicketsQuantity, User = cart.User };
            cart.CartTicket.TicketEvent.AvailableTickets -= cart.TicketsQuantity;
            cart.CartTicket.Quantity = 0;
            orderRepository.Add(order);
            cartRepository.Delete(cart);
            return order;
        }

        public List<Order> GetUsersOrders(string id)
        {
            return orderRepository.GetUsersOrders(id);
        }
    }
}
