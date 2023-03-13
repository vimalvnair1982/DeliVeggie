using MongoDB.Driver;
using MongoDB.Bson;
using DeliVeggie.Model;
using DeliVeggieProducer.Model;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using DeliVeggie.Controllers;

namespace DeliVeggieProducer.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Product> _productCollection;
        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _productCollection = database.GetCollection<Product>(mongoDbSettings.Value.CollectionName);

        }

        public async Task AddProduct(Product product)
        {
            await _productCollection.InsertOneAsync(product);

        }

        public async Task<List<Product>> GetProductLists()
        {
            return await _productCollection.Find(new BsonDocument()).ToListAsync();

        }

        public async Task<Product> GetProduct(Guid Id)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq("Id", Id);
            return await _productCollection.Find(filterDefinition).SingleAsync();

        }
    }
}
