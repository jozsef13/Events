using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface ILocationService
    {
        List<Location> GetAllLocations();
        void CreateLocation(Location location);
        List<Location> GetAllNonPopularLocations();
        Location GetLocationById(Guid id);
        void Update(Location location);
        void Delete(Guid id);
    }
}
