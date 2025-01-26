using MongoDB.Driver;
using test.Models;

namespace test.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<MergedData> _collection;

        public MongoService(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<MergedData>(settings.CollectionName);
        }

        public async Task SaveDataAsync(MergedData data)
        {
            await _collection.InsertOneAsync(data);
        }
    }
}
