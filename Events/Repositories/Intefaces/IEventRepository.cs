using Events.Models;
using System;
using Events.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        List<Event> GetEventByName(string name);
        List<Event> GetEventsByType(Guid typeIdToFind);
        List<Event> GetEventsBySubType(Guid subTypeIdToFind);
        List<Event> GetEventsByArtists(Guid artistIdToFind);
        List<Event> GetEventsByLocation(string locationName);
    }
}
