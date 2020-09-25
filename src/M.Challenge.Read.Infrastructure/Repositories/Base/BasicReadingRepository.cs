using M.Challenge.Read.Domain.Repositories.Base;
using M.Challenge.Read.Infrastructure.Persistence;
using MongoDB.Driver;
using System;
using System.Linq;

namespace M.Challenge.Read.Infrastructure.Repositories.Base
{
    public class BasicReadingRepository<T> : IBasicReadingRepository<T> where T : class
    {
        public IDbContext DbContext { get; }
        public IMongoCollection<T> DbSet { get; }

        public BasicReadingRepository(IDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            DbSet = dbContext.GetCollection<T>(nameof(T));
        }

        public virtual IQueryable<T> GetQuery()
        {
            return DbSet.AsQueryable();
        }
    }
}
