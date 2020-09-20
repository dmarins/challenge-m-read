using Autofac;
using FluentAssertions;
using M.Challenge.Read.Api.Infrastructure.Auth.Handlers;
using M.Challenge.Read.Domain.Repositories.ApiKey;
using M.Challenge.Read.Infrastructure.Repositories.ApiKey;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class AuthModuleTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public AuthModuleTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldResolvingIInMemoryApiKeyRepositoryAsInMemoryApiKeyRepository()
        {
            var instance = _fixture
                .Container
                .Resolve<IInMemoryApiKeyRepository>();

            instance
                .Should()
                .BeOfType<InMemoryApiKeyRepository>();
        }

        [Fact]
        public void ForIInMemoryApiKeyRepository_ShouldBeATransientInstance()
        {
            var instance1 = _fixture
               .Container
               .Resolve<IInMemoryApiKeyRepository>();

            var instance2 = _fixture
                .Container
                .Resolve<IInMemoryApiKeyRepository>();

            instance1
                .Should()
                .NotBe(instance2);
            instance1
                .GetHashCode()
                .Should()
                .NotBe(instance2.GetHashCode());
        }

        [Fact]
        public void ShouldResolvingIAuthorizationHandler()
        {
            var instance = _fixture
                .Container
                .Resolve<IAuthorizationHandler>();

            instance
                .Should()
                .BeOfType<ReadingAuthorizationHandler>();
        }

        [Fact]
        public void ForIAuthorizationHandler_ShouldBeATransientInstance()
        {
            var instance1 = _fixture
               .Container
               .Resolve<IAuthorizationHandler>();

            var instance2 = _fixture
                .Container
                .Resolve<IAuthorizationHandler>();

            instance1
                .Should()
                .NotBe(instance2);
            instance1
                .GetHashCode()
                .Should()
                .NotBe(instance2.GetHashCode());
        }
    }
}
