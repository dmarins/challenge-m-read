using Autofac;
using M.Challenge.Read.Api.Infrastructure.Response;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class ResponseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ResponseFactory>()
                .As<IResponseFactory>()
                .SingleInstance();
        }
    }
}
