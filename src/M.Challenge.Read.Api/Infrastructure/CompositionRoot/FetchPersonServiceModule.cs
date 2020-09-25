using Autofac;
using M.Challenge.Read.Application.Services.Person.Fetch;
using M.Challenge.Read.Domain.Services.Person.Fetch;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class FetchPersonServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<FetchPersonExceptionHandler>()
                .As<IFetchPersonService>();
        }
    }
}
