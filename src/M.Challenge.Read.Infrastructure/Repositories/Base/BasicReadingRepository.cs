using M.Challenge.Read.Domain.Repositories.Base;
using M.Challenge.Read.Infrastructure.Persistence;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public virtual async Task<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            var data = await DbSet.FindAsync(predicate);

            return data.FirstOrDefault();
        }
    }
}
