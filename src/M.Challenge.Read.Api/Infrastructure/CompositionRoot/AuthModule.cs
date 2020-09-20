using Autofac;
using M.Challenge.Read.Api.Infrastructure.Auth.Handlers;
using M.Challenge.Read.Domain.Repositories.ApiKey;
using M.Challenge.Read.Infrastructure.Repositories.ApiKey;
using Microsoft.AspNetCore.Authorization;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class AuthModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<InMemoryApiKeyRepository>()
                .As<IInMemoryApiKeyRepository>();

            builder
                .RegisterType<ReadingAuthorizationHandler>()
                .As<IAuthorizationHandler>();
        }
    }
}
