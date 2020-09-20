using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Read.Api.Controllers;
using M.Challenge.Read.Domain.Constants;
using M.Challenge.Read.Domain.Contracts.Request;
using M.Challenge.Read.Domain.Dtos;
using M.Challenge.Read.UnitTests.Config.AutoData;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Net;
using System.Threading.Tasks;
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

        [Theory]
        [InlineNSubstituteData(ReturnType.InvalidContract, null, (int)HttpStatusCode.BadRequest, ErrorMessageConstants.InvalidContract)]
        [InlineNSubstituteData(ReturnType.Fail, null, (int)HttpStatusCode.InternalServerError, ErrorMessageConstants.Fail)]
        [InlineNSubstituteData(ReturnType.Created, null, (int)HttpStatusCode.Created, null)]
        public async Task Sut_WhenAddPerson_ShouldPerformAsExpected(
            ReturnType expectedReturnType,
            object expectedData,
            int expectedStatusCode,
            string expectedMessage,
            PersonController sut,
            PersonRequest contract)
        {
            var expectedCommandResultDto = new CommandResultDto(
                expectedReturnType,
                expectedData,
                expectedMessage);

            var objectResult =
                new ObjectResult(expectedMessage)
                {
                    StatusCode = expectedStatusCode
                };

            sut.ResponseFactory
                .Return(Arg.Any<CommandResultDto>())
                .Returns(objectResult);

            sut.AddPersonService
                .Process(Arg.Any<PersonCrudDto>())
                .Returns(expectedCommandResultDto);

            var result = await sut.AddPerson(contract);

            result
                .As<ObjectResult>()
                .StatusCode
                .Should()
                .Be(expectedStatusCode);

            result
                .As<ObjectResult>()
                .Value
                .Should()
                .Be(expectedMessage);
        }
    }
}
