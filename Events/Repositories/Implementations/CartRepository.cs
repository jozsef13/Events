using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(EventsDbContext context) : base(context)
        { }

        public Cart GetByForeignUserId(string userId)
        {
            return context.Carts.SingleOrDefault(a => a.UserName == userId);
        }
    }
}
