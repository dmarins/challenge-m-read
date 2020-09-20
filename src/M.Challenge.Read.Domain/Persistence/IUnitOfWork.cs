using System.Threading.Tasks;

namespace M.Challenge.Read.Domain.Persistence
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
