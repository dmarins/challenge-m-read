using System.Linq;

namespace M.Challenge.Read.Domain.Repositories.Base
{
    public interface IBasicReadingRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
    }
}
