using MongoDB.Driver;
using System;

namespace M.Challenge.Read.Infrastructure.Persistence
{
    public class DbContext : IDbContext
    {
        public IMongoDatabase MongoDatabase { get; }
        public IMongoClient MongoClient { get; }

        public DbContext(IMongoDatabase mongoDatabase, IMongoClient mongoClient)
        {
            MongoDatabase = mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase));
            MongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return MongoDatabase.GetCollection<T>(name);
        }
    }
}
