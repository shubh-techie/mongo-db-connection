using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbService
{
    public interface IMongoService
    {
        public Task CreateAsync(Playlist playlist);
        public Task<List<Playlist>> GetAsync();
        public Task AddToPlaylistAsync(string id, string movieId);
        public Task DeleteAsync(string id);
    }
}
