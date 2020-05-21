using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Repositories.Interfaces;

namespace Events.Repositories
{
    public interface IEventTypeRepository : IRepository<EventType>
    {
        EventType GetEventTypeByName(TypeEnum typeName);
    }
}
