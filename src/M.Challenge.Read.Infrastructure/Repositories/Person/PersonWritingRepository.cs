using M.Challenge.Read.Domain.Repositories.Person;
using M.Challenge.Read.Infrastructure.Persistence;
using M.Challenge.Read.Infrastructure.Repositories.Base;

namespace M.Challenge.Read.Infrastructure.Repositories.Person
{
    public class PersonWritingRepository : BasicWritingRepository<Domain.Entities.Person>, IPersonWritingRepository
    {
        public PersonWritingRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
