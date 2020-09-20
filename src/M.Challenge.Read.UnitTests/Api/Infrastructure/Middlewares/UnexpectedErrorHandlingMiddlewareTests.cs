using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Read.Api.Infrastructure.Middlewares;
using M.Challenge.Read.Api.Infrastructure.ProblemDetails;
using M.Challenge.Read.Infrastructure.Logger;
using M.Challenge.Read.UnitTests.Config.AutoData;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.Middlewares
{
    public class UnexpectedErrorHandlingMiddlewareTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldHaveGuardClauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(UnexpectedErrorHandlingMiddleware).GetConstructors());
        }

        [Fact]
        public async Task Sut_WhenUnexpectedErrorIsRaised_ShouldHandleItToCustomErrorResponseAndCorrectHttpStatus()
        {
            var middleware = new UnexpectedErrorHandlingMiddleware(
                next: (innerHttpContext) =>
                {
                    throw new InvalidOperationException("Test", new Exception("Test"));
                },
                new Logger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var customErrorResponse = JsonConvert.DeserializeObject<InternalServerErrorProblemDetails>(streamText);

            customErrorResponse
                .Should()
                .BeEquivalentTo(
                    new InternalServerErrorProblemDetails
                    {
                        Detail = "Test"
                    });

            context
                .Response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.InternalServerError);

            context
                .Response
                .ContentType
                .Should()
                .Be("application/problem+json");
        }
    }
}
