using M.Challenge.Read.Domain.Persistence;
using System;
using System.Threading.Tasks;

namespace M.Challenge.Read.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbContext DbContext { get; }

        public UnitOfWork(IDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> Commit()
        {
            return await DbContext.SaveChanges() > 0;
        }
    }
}
