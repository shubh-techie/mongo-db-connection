using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbService
{
    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<Playlist> _playlistCollection;
        private readonly IOptions<MongoOptions> _options;
        public MongoService(IOptions<MongoOptions> mongoOptions)
        {
            _options = mongoOptions;
            _playlistCollection = GetMongoCollection();
        }

        private IMongoCollection<Playlist> GetMongoCollection()
        {
            MongoClient client = new MongoClient(_options.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(_options.Value.DatabaseName);
            return database.GetCollection<Playlist>(_options.Value.CollectionName);
        }

        public async Task CreateAsync(Playlist playlist)
        {
            await _playlistCollection.InsertOneAsync(playlist);
            return;
        }

        public async Task<List<Playlist>> GetAsync()
        {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddToPlaylistAsync(string id, string movieId)
        {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("movieIds", movieId);
            await _playlistCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            await _playlistCollection.DeleteOneAsync(filter);
            return;
        }
    }
}