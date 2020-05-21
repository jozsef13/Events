using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Models;
using Events.Repositories.Interfaces;

namespace Events.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetUsersOrders(string id);
    }
}
