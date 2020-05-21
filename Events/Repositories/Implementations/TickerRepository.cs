using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class TickerRepository : BaseRepository<Ticket>, ITicketRepository
    {

        public TickerRepository(EventsDbContext context) : base(context)
        { }
    }
}
