using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class EventSubTypeRepository : BaseRepository<EventSubType>, IEventSubTypeRepository
    {
        public EventSubTypeRepository(EventsDbContext context) : base(context)
        { }

        public EventSubType GetSubTypeByName(SubTypeEnum subTypeName)
        {
            var subType = context.EventSubTypes.SingleOrDefault(s => s.Name == subTypeName);
            subType.TypeParent = context.EventTypes.Find(subType.TypeParentId);
            return subType;
        }
    }
}
