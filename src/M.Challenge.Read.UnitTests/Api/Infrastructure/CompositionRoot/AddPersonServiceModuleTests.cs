using Autofac;
using FluentAssertions;
using M.Challenge.Read.Application.Services.Person.Add;
using M.Challenge.Read.Domain.Services.Person;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class AddPersonServiceModuleTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public AddPersonServiceModuleTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Resolve_ShouldResolvingIAddPersonServiceAsAddPersonExceptionHandler_ShoulBeADecorator()
        {
            var instance = _fixture.Container.Resolve<IAddPersonService>();

            instance
                .Should()
                .BeOfType<AddPersonExceptionHandler>();

            instance
                .As<AddPersonExceptionHandler>()
                .Decorated
                .Should()
                .BeOfType<AddPersonValidator>();

            instance
                .As<AddPersonExceptionHandler>()
                .Decorated
                .As<AddPersonValidator>()
                .Decorated
                .Should()
                .BeOfType<AddPerson>();
        }

        [Fact]
        public void ForIAddPersonService_ShouldBeATransientInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IAddPersonService>();

            var instance2 = _fixture
                .Container
                .Resolve<IAddPersonService>();

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