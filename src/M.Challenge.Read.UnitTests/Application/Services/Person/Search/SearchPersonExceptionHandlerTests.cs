using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Read.Application.Services.Person.Search;
using M.Challenge.Read.UnitTests.Config.AutoData;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace M.Challenge.Read.UnitTests.Application.Services.Person.Search
{
    public class SearchPersonExceptionHandlerTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldHaveGuardClauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(SearchPersonExceptionHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Sut_WhenRepositoryNotThrowsException_ReturnsList(
            SearchPersonExceptionHandler sut,
            List<Read.Domain.Entities.Person> people)
        {
            sut
                .PersonReadingRepository
                .GetQuery()
                .Returns(people.AsQueryable());

            var result = sut.Process();

            result
                .Should()
                .HaveCount(3);

            sut
                .Logger
                .Received(0)
                .Error(Arg.Any<string>(), Arg.Any<Exception>());
        }

        [Theory, AutoNSubstituteData]
        public void Sut_WhenRepositoryThrowsException_ReturnsNull(
            SearchPersonExceptionHandler sut)
        {
            sut
                .PersonReadingRepository
                .GetQuery()
                .Throws<Exception>();

            var result = sut.Process();

            result
                .Should()
                .BeNull();

            sut
                .Logger
                .Received(1)
                .Error("Erro ao buscar pessoa.", Arg.Any<Exception>());
        }
    }
}
