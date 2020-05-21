using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {

        public EventRepository(EventsDbContext context) : base(context)
        { }

        public List<Event> GetEventByName(string name)
        {
            var returnedEvents = GetAll();

            if (!String.IsNullOrEmpty(name))
            {
                returnedEvents = returnedEvents.Where(s => s.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            foreach (var iEvent in returnedEvents)
            {
                iEvent.Artist = context.Artists.Find(iEvent.ArtistId);
                iEvent.Location = context.Locations.Find(iEvent.LocationId);
                iEvent.Type = context.EventTypes.Find(iEvent.TypeId);
                iEvent.SubType = context.EventSubTypes.Find(iEvent.SubTypeId);
            }

            return returnedEvents;
        }

        public List<Event> GetEventsByArtists(Guid artistIdToFind)
        {
            List<Event> events = context.Events.Where(e => e.ArtistId == artistIdToFind).ToList();
            foreach (var iEvent in events)
            {
                iEvent.Artist = context.Artists.Find(iEvent.ArtistId);
                iEvent.Location = context.Locations.Find(iEvent.LocationId);
                iEvent.Type = context.EventTypes.Find(iEvent.TypeId);
                iEvent.SubType = context.EventSubTypes.Find(iEvent.SubTypeId);
            }

            return events;
        }

        public List<Event> GetEventsByLocation(string locationName)
        {
            Location location = context.Locations.Where(e => e.City == locationName).SingleOrDefault();
            List<Event> events = context.Events.Where(e => e.LocationId == location.Id).ToList();

            foreach (var iEvent in events)
            {
                iEvent.Artist = context.Artists.Find(iEvent.ArtistId);
                iEvent.Location = context.Locations.Find(iEvent.LocationId);
                iEvent.Type = context.EventTypes.Find(iEvent.TypeId);
                iEvent.SubType = context.EventSubTypes.Find(iEvent.SubTypeId);
            }

            return events;
        }

        public List<Event> GetEventsBySubType(Guid subTypeIdToFind)
        {
            List<Event> events = context.Events.Where(e => e.SubTypeId == subTypeIdToFind).ToList();
            
            foreach (var iEvent in events)
            {
                iEvent.Artist = context.Artists.Find(iEvent.ArtistId);
                iEvent.Location = context.Locations.Find(iEvent.LocationId);
                iEvent.Type = context.EventTypes.Find(iEvent.TypeId);
                iEvent.SubType = context.EventSubTypes.Find(iEvent.SubTypeId);
            }

            return events;
        }

        public List<Event> GetEventsByType(Guid typeIdToFind)
        {
            List<Event> events = context.Events.Where(e => e.TypeId == typeIdToFind).ToList();
            foreach (var iEvent in events)
            {
                iEvent.Artist = context.Artists.Find(iEvent.ArtistId);
                iEvent.Location = context.Locations.Find(iEvent.LocationId);
                iEvent.Type = context.EventTypes.Find(iEvent.TypeId);
                iEvent.SubType = context.EventSubTypes.Find(iEvent.SubTypeId);
            }

            return events;
        }
    }
}
