using M.Challenge.Read.Domain.Entities;
using M.Challenge.Read.Domain.Repositories.ApiKey;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace M.Challenge.Read.Infrastructure.Repositories.ApiKey
{
    [ExcludeFromCodeCoverage]
    public class InMemoryApiKeyRepository : IInMemoryApiKeyRepository
    {
        private readonly IDictionary<Guid, Domain.Entities.ApiKey> _apiKeys;

        public InMemoryApiKeyRepository()
        {
            var existingApiKeys = new List<Domain.Entities.ApiKey>
            {
                new Domain.Entities.ApiKey(1, "Unidade RJ", Guid.Parse("C5BFF7F0-B4DF-475E-A331-F737424F013C"), new DateTime(2020, 01, 01),
                    new List<string>
                    {
                        Role.Reading,
                    }),
                new Domain.Entities.ApiKey(2, "Unidade SP", Guid.Parse("5908D47C-85D3-4024-8C2B-6EC9464398AD"), new DateTime(2020, 01, 01),
                    new List<string>
                    {
                        Role.Reading
                    }),
                new Domain.Entities.ApiKey(3, "Unidade MG", Guid.Parse("06795D9D-A770-44B9-9B27-03C6ABDB1BAE"), new DateTime(2020, 01, 01),
                    new List<string>
                    {
                        Role.Reading
                    })
            };

            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<Domain.Entities.ApiKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(Guid.Parse(providedApiKey), out var key);

            return Task.FromResult(key);
        }
    }
}
