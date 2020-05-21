using System;
using Events.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Models;

namespace Events.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetByForeignUserId(string userId);
    }
}
