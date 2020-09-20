using Autofac;
using FluentAssertions;
using M.Challenge.Read.Api.Infrastructure.Response;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class ResponseModuleTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public ResponseModuleTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldResolvingIResponseFactoryAsResponseFactory()
        {
            var instance = _fixture
                .Container
                .Resolve<IResponseFactory>();

            instance
                .Should()
                .BeOfType<ResponseFactory>();
        }

        [Fact]
        public void ForIResponseFactory_ShouldBeASingleInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IResponseFactory>();

            var instance2 = _fixture
                .Container
                .Resolve<IResponseFactory>();

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