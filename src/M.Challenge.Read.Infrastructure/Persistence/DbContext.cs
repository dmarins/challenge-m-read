using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M.Challenge.Read.Infrastructure.Persistence
{
    public class DbContext : IDbContext
    {
        public IMongoDatabase MongoDatabase { get; }
        public IMongoClient MongoClient { get; }

        private IClientSessionHandle _session { get; set; }
        private List<Func<Task>> _commands { get; }

        public DbContext(IMongoDatabase mongoDatabase, IMongoClient mongoClient)
        {
            MongoDatabase = mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase));
            MongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

            _commands = new List<Func<Task>>();
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return MongoDatabase.GetCollection<T>(name);
        }

        public async Task<int> SaveChanges()
        {
            using (_session = await MongoClient.StartSessionAsync())
            {
                _session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await _session.CommitTransactionAsync();
            }

            return _commands.Count;
        }
    }
}
