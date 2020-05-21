using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface IEventService
    {
        void CreateEvent(string name, string description, DateTime? dateTime, int maximumTickets, string photoPath, string locationCity, string locationCountry, SubTypeEnum subTypeName, string artistName, Ticket ticket);
        Event GetEventById(Guid id);
        List<Event> GetEventByName(string name);
        List<Event> GetEventByArtist(Guid artistId);
        List<Event> GetAllEvents();
        List<Event> GetEventsByType(Guid typeIdToFind);
        List<Event> GetEventsBySubType(Guid subTypeIdToFind);
        List<Event> GetEventsByLocation(string locationName);
        Event UpdateEvent(Guid id, string name, string description, DateTime? dateTime, int maximumTickets, string photoPath, string locationCity, string locationCountry, SubTypeEnum subTypeName, string artistName, Ticket ticket);
        void Delete(Guid id);
    }
}
