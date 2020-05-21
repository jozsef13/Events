using Events.Models;
using Events.Repositories;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class EventTypeService : IEventTypeService
    {
        private IEventTypeRepository eventTypeRepository;

        public EventTypeService(IEventTypeRepository eventTypeRepository)
        {
            this.eventTypeRepository = eventTypeRepository;
        }

        public List<EventType> GetAllTypes()
        {
            return eventTypeRepository.GetAll();
        }
    }
}
