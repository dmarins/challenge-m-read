using Autofac;
using FluentAssertions;
using M.Challenge.Read.Infrastructure.Persistence;
using MongoDB.Driver;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class DbContextModuleTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public DbContextModuleTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldResolvingIMongoClientAsMongoClient()
        {
            var instance = _fixture
                .Container
                .Resolve<IMongoClient>();

            instance
                .Should()
                .BeOfType<MongoClient>();
        }

        [Fact]
        public void ForIMongoClient_ShouldBeASingleInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IMongoClient>();

            var instance2 = _fixture
                .Container
                .Resolve<IMongoClient>();

            instance1
                .Should()
                .Be(instance2);
            instance1
                .GetHashCode()
                .Should()
                .Be(instance2.GetHashCode());
        }

        [Fact]
        public void ForIMongoDatabase_ShouldBeASingleInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IMongoDatabase>();

            var instance2 = _fixture
                .Container
                .Resolve<IMongoDatabase>();

            instance1
                .Should()
                .Be(instance2);
            instance1
                .GetHashCode()
                .Should()
                .Be(instance2.GetHashCode());
        }

        [Fact]
        public void ShouldResolvingIDbContextAsDbContext()
        {
            var instance = _fixture
                .Container
                .Resolve<IDbContext>();

            instance
                .Should()
                .BeOfType<DbContext>();
        }

        [Fact]
        public void ForIDbContext_ShouldBeASingleInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IDbContext>();

            var instance2 = _fixture
                .Container
                .Resolve<IDbContext>();

            instance1
                .Should()
                .Be(instance2);
            instance1
                .GetHashCode()
                .Should()
                .Be(instance2.GetHashCode());
        }
    }
}
