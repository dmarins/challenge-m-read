using MongoDB.Driver;

namespace M.Challenge.Read.Infrastructure.Persistence
{
    public interface IDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
