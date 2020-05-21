using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface IOrderService
    {
        Order Create(Cart cart);
        List<Order> GetUsersOrders(string id);
    }
}
