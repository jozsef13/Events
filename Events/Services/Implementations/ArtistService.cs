using Events.Models;
using Events.Repositories;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class ArtistService : IArtistService
    {
        private IArtistsRepository artistRepository;

        public ArtistService(IArtistsRepository artistRepository)
        {
            this.artistRepository = artistRepository;
        }

        public void CreateArtist(string name, string description, string imagePath)
        {
            artistRepository.Add(new Artist { Name = name, Description = description, PhotoPath = imagePath });
        }

        public List<Artist> GetAllArtists()
        {
            return artistRepository.GetAll();
        }

        public List<Artist> GetArtistsByName(string name)
        {
            return artistRepository.GetArtistsByName(name);
        }

        public Artist GetArtistByName(string name)
        {
            return artistRepository.GetArtistByName(name);
        }

        public void Update(Artist artist)
        {
            if (artist == null)
            {
                throw new Exception("Invalid object!");
            }

            artistRepository.Update(artist);
        }

        public Artist GetArtistById(Guid id)
        {
            var artist = artistRepository.GetById(id);

            if(artist == null)
            {
                throw new Exception("Entity not found!");
            }

            return artist;
        }

        public void Delete(Guid id)
        {
            Artist artist = GetArtistById(id);
            artistRepository.Delete(artist);
        }
    }
}
