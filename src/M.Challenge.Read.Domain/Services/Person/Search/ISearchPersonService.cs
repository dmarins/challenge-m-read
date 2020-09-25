using System.Collections.Generic;

namespace M.Challenge.Read.Domain.Services.Person.Search
{
    public interface ISearchPersonService
    {
        List<Entities.Person> Process();
    }
}
