using System.Threading.Tasks;

namespace M.Challenge.Read.Domain.Repositories.Base
{
    public interface IBasicWritingRepository<T> where T : class
    {
        Task<T> Add(T entity);
    }
}
