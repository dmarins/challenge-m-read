using M.Challenge.Read.Domain.Repositories.Person;
using M.Challenge.Read.Infrastructure.Persistence;
using M.Challenge.Read.Infrastructure.Repositories.Base;

namespace M.Challenge.Read.Infrastructure.Repositories.Person
{
    public class PersonReadingRepository : BasicReadingRepository<Domain.Entities.Person>, IPersonReadingRepository
    {
        public PersonReadingRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
