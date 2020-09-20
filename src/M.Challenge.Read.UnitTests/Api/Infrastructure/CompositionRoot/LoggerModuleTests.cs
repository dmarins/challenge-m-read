using Autofac;
using FluentAssertions;
using M.Challenge.Read.Domain.Logger;
using M.Challenge.Read.Infrastructure.Logger;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class LoggerModuleTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public LoggerModuleTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldResolvingILoggerAsLogger()
        {
            var instance = _fixture
                .Container
                .Resolve<ILogger>();

            instance
                .Should()
                .BeOfType<Logger>();
        }

        [Fact]
        public void ForILogger_ShouldBeASingleInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<ILogger>();

            var instance2 = _fixture
                .Container
                .Resolve<ILogger>();

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
