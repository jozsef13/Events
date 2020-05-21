using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class EventTypeRepository : BaseRepository<EventType>, IEventTypeRepository
    {
        public EventTypeRepository(EventsDbContext context) : base(context)
        { }


        public EventType GetEventTypeByName(TypeEnum typeName)
        {
            return context.EventTypes.SingleOrDefault(s => s.Name == typeName);
        }
    }
}
