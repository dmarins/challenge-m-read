using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Read.Api.Controllers;
using M.Challenge.Read.Domain.Entities;
using M.Challenge.Read.UnitTests.Config.AutoData;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Controllers
{
    public class PersonControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldHaveGuardClauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(PersonController).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Sut_WhenSearchPerson_ShouldPerformAsExpected(PersonController sut, List<Person> people)
        {
            sut.SearchPersonService
                .Process()
                .Returns(people);

            var result = sut.Search();

            result
                .As<List<Person>>()
                .Should()
                .HaveCount(3);
        }
    }
}
