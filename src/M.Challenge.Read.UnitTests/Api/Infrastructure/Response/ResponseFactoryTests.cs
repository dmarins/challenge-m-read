using FluentAssertions;
using M.Challenge.Read.Api.Infrastructure.Response;
using M.Challenge.Read.Domain.Constants;
using M.Challenge.Read.Domain.Dtos;
using M.Challenge.Read.UnitTests.Config.AutoData;
using System.Net;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.Response
{
    public class ResponseFactoryTests
    {
        [Theory]
        [InlineNSubstituteData(ReturnType.InvalidContract, null, (int)HttpStatusCode.BadRequest, ErrorMessageConstants.InvalidContract)]
        [InlineNSubstituteData(ReturnType.Fail, null, (int)HttpStatusCode.InternalServerError, ErrorMessageConstants.Fail)]
        [InlineNSubstituteData(ReturnType.Undefined, null, (int)HttpStatusCode.BadRequest, ErrorMessageConstants.InvalidContract)]
        [InlineNSubstituteData(ReturnType.Created, null, (int)HttpStatusCode.Created, null)]
        public void Sut_WhenFactoryReturnsAccordingToTheDto_ShouldPerformAsExpected(
            ReturnType expectedReturnType,
            object expectedData,
            int expectedStatusCode,
            string expectedMessage,
            ResponseFactory sut)
        {
            var expectedQueryResultDto = new CommandResultDto(
                expectedReturnType,
                expectedData,
                expectedMessage);

            var result = sut.Return(expectedQueryResultDto);

            result
                .StatusCode
                .Should()
                .Be(expectedStatusCode);

            result
                .Value
                .Should()
                .Be(expectedMessage);
        }
    }
}
