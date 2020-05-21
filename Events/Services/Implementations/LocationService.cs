using Events.Models;
using Events.Repositories;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class LocationService : ILocationService
    {
        private ILocationRepository locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public void CreateLocation(Location location)
        {
            if(location == null)
            {
                throw new Exception("Invalid location!");
            }

            locationRepository.Add(location);
        }

        public void Delete(Guid id)
        {
            Location location = GetLocationById(id);
            locationRepository.Delete(location);
        }

        public List<Location> GetAllLocations()
        {
            return locationRepository.GetAll();
        }

        public List<Location> GetAllNonPopularLocations()
        {
            var locations = locationRepository.GetAll();
            var insideLocations = new List<Location>();
            foreach(var location in locations)
            {
                if(location.City != "Targu-Mures" && location.City != "Bucharest" && location.City != "Craiova")
                {
                    insideLocations.Add(location);
                }
            }

            return insideLocations;
        }

        public Location GetLocationById(Guid id)
        {
            return locationRepository.GetById(id);
        }

        public void Update(Location location)
        {
            if(location == null)
            {
                throw new Exception("Invalid object!");
            }

            locationRepository.Update(location);
        }
    }
}
