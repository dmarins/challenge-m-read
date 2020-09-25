using M.Challenge.Read.Domain.Logger;
using M.Challenge.Read.Domain.Repositories.Person;
using M.Challenge.Read.Domain.Services.Person.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M.Challenge.Read.Application.Services.Person.Search
{
    public class SearchPersonExceptionHandler : ISearchPersonService
    {
        public ILogger Logger { get; }
        public IPersonReadingRepository PersonReadingRepository { get; }

        public SearchPersonExceptionHandler(ILogger logger,
            IPersonReadingRepository personReadingRepository)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            PersonReadingRepository = personReadingRepository ?? throw new ArgumentNullException(nameof(personReadingRepository));
        }

        public List<Domain.Entities.Person> Process()
        {
            try
            {
                return PersonReadingRepository
                    .GetQuery()
                    .ToList();
            }
            catch (Exception e)
            {
                Logger.Error("Erro ao buscar pessoa.", e);
                return null;
            }
        }
    }
}
