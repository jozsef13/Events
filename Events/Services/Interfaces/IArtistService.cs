using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface IArtistService
    {
        List<Artist> GetAllArtists();
        Artist GetArtistByName(string name);
        List<Artist> GetArtistsByName(string name);
        void CreateArtist(string name, string description, string imagePath);
        void Update(Artist artist);
        Artist GetArtistById(Guid id);
        void Delete(Guid id);
    }
}
