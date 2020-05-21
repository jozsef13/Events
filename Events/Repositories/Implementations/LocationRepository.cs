using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {

        public LocationRepository(EventsDbContext context) : base(context)
        { }

        public Location GetLocationByCityAndCountry(string city, string country)
        {
            return context.Locations.SingleOrDefault(l => l.City == city && l.Country == country);
        }
    }
}
