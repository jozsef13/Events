using Events.Models;
using Events.Repositories;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class EventSubTypeService : IEventSubTypeService
    {
        private IEventSubTypeRepository eventSubTypeRepository;

        public EventSubTypeService(IEventSubTypeRepository eventSubTypeRepository)
        {
            this.eventSubTypeRepository = eventSubTypeRepository;
        }

        public List<EventSubType> GetAllSubTypes()
        {
            return eventSubTypeRepository.GetAll();
        }

        public EventSubType GetById(Guid subTypeId)
        {
            return eventSubTypeRepository.GetById(subTypeId);
        }
    }
}
