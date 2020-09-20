using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace M.Challenge.Read.Domain.Repositories.Base
{
    public interface IBasicReadingRepository<T> where T : class
    {
        Task<T> GetBy(Expression<Func<T, bool>> predicate);
    }
}
