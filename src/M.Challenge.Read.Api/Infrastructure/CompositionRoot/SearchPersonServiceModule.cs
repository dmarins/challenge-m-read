using Autofac;
using M.Challenge.Read.Application.Services.Person.Search;
using M.Challenge.Read.Domain.Services.Person.Search;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class SearchPersonServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SearchPersonExceptionHandler>()
                .As<ISearchPersonService>();
        }
    }
}
