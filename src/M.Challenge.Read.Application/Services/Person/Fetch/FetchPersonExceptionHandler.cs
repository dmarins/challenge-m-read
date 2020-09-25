using M.Challenge.Read.Domain.Logger;
using M.Challenge.Read.Domain.Repositories.Person;
using M.Challenge.Read.Domain.Services.Person.Fetch;
using System;

namespace M.Challenge.Read.Application.Services.Person.Fetch
{
    public class FetchPersonExceptionHandler : IFetchPersonService
    {
        public ILogger Logger { get; }
        public IPersonReadingRepository PersonReadingRepository { get; }

        public FetchPersonExceptionHandler(ILogger logger,
            IPersonReadingRepository personReadingRepository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            PersonReadingRepository = personReadingRepository ?? throw new ArgumentNullException(nameof(personReadingRepository));
        }

        public Domain.Entities.Person Process(string id)
        {
            try
            {
                return PersonReadingRepository.GetBy(x => x.Id == id);
            }
            catch (Exception e)
            {
                Logger.Error("Erro ao obter pessoa.", e);
                return null;
            }
        }
    }
}
