using MasterCatalog.Domain.Entities;
using MasterCatalog.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MasterCatalog.Infrastructure.Persistence;

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings) {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<MasterProduct> Products => 
            _database.GetCollection<MasterProduct>(
                "products"
            );

    }
