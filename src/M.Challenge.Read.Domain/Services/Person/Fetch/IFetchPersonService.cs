namespace M.Challenge.Read.Domain.Services.Person.Fetch
{
    public interface IFetchPersonService
    {
        Entities.Person Process(string id);
    }
}
