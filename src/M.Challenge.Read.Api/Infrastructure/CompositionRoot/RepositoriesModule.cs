using Autofac;
using M.Challenge.Read.Domain.Repositories.Person;
using M.Challenge.Read.Infrastructure.Repositories.Person;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PersonReadingRepository>()
                .As<IPersonReadingRepository>();
        }
    }
}
