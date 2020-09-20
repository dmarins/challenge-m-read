using Autofac;
using FluentAssertions;
using M.Challenge.Read.Domain.Repositories.Person;
using M.Challenge.Read.Infrastructure.Repositories.Person;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class RepositoriesModuleTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public RepositoriesModuleTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldResolvingIPersonReadingRepositoryAsPersonReadingRepository()
        {
            var instance = _fixture
                .Container
                .Resolve<IPersonReadingRepository>();

            instance
                .Should()
                .BeOfType<PersonReadingRepository>();
        }

        [Fact]
        public void ForIPersonReadingRepository_ShouldBeATransientInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IPersonReadingRepository>();

            var instance2 = _fixture
                .Container
                .Resolve<IPersonReadingRepository>();

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
