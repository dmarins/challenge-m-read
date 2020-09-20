using Autofac;
using M.Challenge.Read.Domain.Logger;
using M.Challenge.Read.Infrastructure.Logger;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Logger>()
                .As<ILogger>()
                .SingleInstance();
        }
    }
}
