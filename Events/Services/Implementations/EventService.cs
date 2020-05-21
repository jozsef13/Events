using Events.Models;
using Events.Repositories;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class EventService : IEventService
    {
        private IEventRepository eventRepository;
        private ILocationRepository locationRepository;
        private IArtistsRepository artistsRepository;
        private IEventTypeRepository eventTypeRepository;
        private IEventSubTypeRepository eventSubTypeRepository;
        private ITicketRepository ticketRepository;

        public EventService(IEventRepository eventRepository, ILocationRepository locationRepository, IArtistsRepository artistsRepository, IEventTypeRepository eventTypeRepository, IEventSubTypeRepository eventSubTypeRepository, ITicketRepository ticketRepository)
        {
            this.eventRepository = eventRepository;
            this.locationRepository = locationRepository;
            this.artistsRepository = artistsRepository;
            this.eventTypeRepository = eventTypeRepository;
            this.eventSubTypeRepository = eventSubTypeRepository;
            this.ticketRepository = ticketRepository;
        }

        public void CreateEvent(string name, string description, DateTime? dateTime, int maximumTickets, string photoPath, string locationCity, string locationCountry, SubTypeEnum subTypeName, string artistName, Ticket ticket)
        {
            var location = locationRepository.GetLocationByCityAndCountry(locationCity, locationCountry);
            if(location == null)
            {
                throw new Exception("The location does not exist!");
            }
            
            var subType = eventSubTypeRepository.GetSubTypeByName(subTypeName);
            if (subType == null)
            {
                throw new Exception("The subtype does not exist!");
            }

            var typeName = subType.TypeParent.Name;
            var type = eventTypeRepository.GetEventTypeByName(typeName);
            if (type == null)
            {
                throw new Exception("The type does not exist!");
            }
            var artist = artistsRepository.GetArtistByName(artistName);
            if (artist == null)
            {
                throw new Exception("The artist does not exist!");
            }

            if(ticket == null)
            {
                throw new Exception("The ticket does not exist!");
            }

            Event newEvent = new Event { Id = Guid.NewGuid(), Name = name, Description = description, DateTime = dateTime, MaximumTickets = maximumTickets, CoverPhotoPath = photoPath, Location = location, Type = type, SubType = subType, Artist = artist, AvailableTickets = maximumTickets, Ticket = ticket };
            ticket.TicketEvent = newEvent;
            ticket.EventId = newEvent.Id;
            eventRepository.Add(newEvent);
        }

        public void Delete(Guid id)
        {
            var iEvent = GetEventById(id);
            var ticket = ticketRepository.GetById(iEvent.TicketId);
            eventRepository.Delete(iEvent);
            ticketRepository.Delete(ticket);
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events = eventRepository.GetAll();
            foreach(Event iEvent in events)
            {
                iEvent.Artist = artistsRepository.GetById(iEvent.ArtistId);
                iEvent.Location = locationRepository.GetById(iEvent.LocationId);
                iEvent.SubType = eventSubTypeRepository.GetById(iEvent.SubTypeId);
                iEvent.Type = eventTypeRepository.GetById(iEvent.TypeId);
            }

            return events;
        }

        public List<Event> GetEventByArtist(Guid artistId)
        {
            return eventRepository.GetEventsByArtists(artistId);
        }

        public Event GetEventById(Guid id)
        {
            Event searchedEvent = eventRepository.GetById(id);
            if(searchedEvent == null)
            {
                throw new Exception("This event does not exist!");
            }
            searchedEvent.Artist = artistsRepository.GetById(searchedEvent.ArtistId);
            searchedEvent.Location = locationRepository.GetById(searchedEvent.LocationId);
            searchedEvent.SubType = eventSubTypeRepository.GetById(searchedEvent.SubTypeId);
            searchedEvent.Type = eventTypeRepository.GetById(searchedEvent.TypeId);
            return searchedEvent;
        }

        public List<Event> GetEventByName(string name)
        {
            var searchedEvent = eventRepository.GetEventByName(name);
            if (searchedEvent == null)
            {
                throw new Exception("This event does not exist!");
            }
            return searchedEvent;
        }

        public List<Event> GetEventsByLocation(string locationName)
        {
            return eventRepository.GetEventsByLocation(locationName);
        }

        public List<Event> GetEventsBySubType(Guid subTypeIdToFind)
        {
            return eventRepository.GetEventsBySubType(subTypeIdToFind);
        }

        public List<Event> GetEventsByType(Guid typeIdToFind)
        {
            return eventRepository.GetEventsByType(typeIdToFind);
        }

        public Event UpdateEvent(Guid id, string name, string description, DateTime? dateTime, int maximumTickets, string photoPath, string locationCity, string locationCountry, SubTypeEnum subTypeName, string artistName, Ticket ticket)
        {
            var oldEvent = GetEventById(id);
            if(oldEvent != null)
            {
                var location = locationRepository.GetLocationByCityAndCountry(locationCity, locationCountry);
                if (location == null)
                {
                    throw new Exception("The location does not exist!");
                }

                var subType = eventSubTypeRepository.GetSubTypeByName(subTypeName);
                if (subType == null)
                {
                    throw new Exception("The subtype does not exist!");
                }

                var typeName = subType.TypeParent.Name;
                var type = eventTypeRepository.GetEventTypeByName(typeName);
                if (type == null)
                {
                    throw new Exception("The type does not exist!");
                }
                var artist = artistsRepository.GetArtistByName(artistName);
                if (artist == null)
                {
                    throw new Exception("The artist does not exist!");
                }

                var oldTicket = ticketRepository.GetById(oldEvent.TicketId);
                if(oldTicket == null)
                {
                    throw new Exception("The ticket does not exist!");
                }

                oldTicket.Price = ticket.Price;

                oldEvent.Name = name;
                oldEvent.Artist = artist;
                oldEvent.MaximumTickets = maximumTickets;
                oldEvent.AvailableTickets = oldEvent.MaximumTickets;
                oldEvent.CoverPhotoPath = photoPath;
                oldEvent.DateTime = dateTime;
                oldEvent.Description = description;
                oldEvent.Location = location;
                oldEvent.SubType = subType;
                oldEvent.Type = type;
                oldEvent.Ticket = oldTicket;
            }

            eventRepository.Update(oldEvent);
            return oldEvent;
        }
    }
}
