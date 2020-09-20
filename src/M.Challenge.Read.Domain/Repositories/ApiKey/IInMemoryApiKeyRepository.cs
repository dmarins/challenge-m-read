using System.Threading.Tasks;


namespace M.Challenge.Read.Domain.Repositories.ApiKey
{
    public interface IInMemoryApiKeyRepository
    {
        Task<Entities.ApiKey> Execute(string providedApiKey);
    }
}
