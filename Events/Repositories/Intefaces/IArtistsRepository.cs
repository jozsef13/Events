using Events.Models;
using Events.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Repositories
{
    public interface IArtistsRepository : IRepository<Artist>
    {
        List<Artist> GetArtistsByName(string artistName);
        Artist GetArtistByName(string artistName);
    }
}
