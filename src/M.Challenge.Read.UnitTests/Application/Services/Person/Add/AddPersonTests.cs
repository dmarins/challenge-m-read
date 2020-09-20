using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Read.Application.Services.Person.Add;
using M.Challenge.Read.Domain.Dtos;
using M.Challenge.Read.UnitTests.Config.AutoData;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace M.Challenge.Read.UnitTests.Application.Services.Person.Add
{
    public class AddPersonTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldHaveGuardClauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(AddPerson).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_WhenPersonIsPersisted_ReturnsCreated(
            AddPerson sut,
            PersonCrudDto dto,
            Read.Domain.Entities.Person person)
        {
            var commandResultDto = new CommandResultDto().Created();

            sut
                .PersonWritingRepository
                .Add(Arg.Any<Read.Domain.Entities.Person>())
                .Returns(person);

            var result = await sut.Process(dto);

            result.Data.Should().Be(person);
            result.Message.Should().Be(commandResultDto.Message);
            result.ReturnType.Should().Be(ReturnType.Created);

            await sut
                .UnitOfWork
                .Received(1)
                .Commit();
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_WhenRepositoryThrowsException_ThrowsException(
            AddPerson sut,
            PersonCrudDto dto)
        {
            var commandResultDto = new CommandResultDto().Created();

            sut
                .PersonWritingRepository
                .Add(Arg.Any<Read.Domain.Entities.Person>())
                .Throws<Exception>();

            await Assert.ThrowsAsync<Exception>(() => sut.Process(dto));

            await sut
                .UnitOfWork
                .Received(0)
                .Commit();
        }
    }
}
