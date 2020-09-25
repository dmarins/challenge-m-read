using System;
using System.Linq;
using System.Linq.Expressions;

namespace M.Challenge.Read.Domain.Repositories.Base
{
    public interface IBasicReadingRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        T GetBy(Expression<Func<T, bool>> predicate);
    }
}
