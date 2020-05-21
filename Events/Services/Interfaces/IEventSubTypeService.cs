using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface IEventSubTypeService
    {
        List<EventSubType> GetAllSubTypes();
        EventSubType GetById(Guid subTypeId);
    }
}
