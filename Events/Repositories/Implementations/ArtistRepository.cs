using Events.Data;
using Events.Models;
using Events.Repositories.Implementations;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistsRepository
    {
        public ArtistRepository(EventsDbContext context) : base(context)
        { }

        public Artist GetArtistByName(string artistName)
        {
            return context.Artists.SingleOrDefault(a => a.Name == artistName);
        }

        public List<Artist> GetArtistsByName(string artistName)
        {
            var artists = GetAll();

            if (!String.IsNullOrEmpty(artistName))
            {
                artists = artists.Where(s => s.Name.Contains(artistName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            return artists;
        }
    }
}
