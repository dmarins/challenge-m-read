using AutoFixture.Idioms;
using M.Challenge.Read.Api.Infrastructure.Middlewares;
using M.Challenge.Read.UnitTests.Config.AutoData;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.Middlewares
{
    public class HttpLoggingMiddlewareTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldHaveGuardClauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(HttpLoggingMiddleware).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_ShouldBeLogRequestAndResponse(HttpLoggingMiddleware sut, DefaultHttpContext context)
        {
            context.Request.Headers.Add("Content-Type", "application/json");

            await sut.InvokeAsync(context);

            sut
                .Logger
                .Received(1)
                .Info("[Incoming Request] {Method} {Url}\n\n{Headers}\n\n{Body}",
                    Arg.Any<object[]>());
            sut
                .Logger
                .Received(1)
                .Info("[Incoming Response] {Method} {Url} {StatusCode} {StatusCodeDescription}\n\n{Headers}\n\n{Body}",
                    Arg.Any<object[]>());

        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_WhenRequestHasntHeaders_ShouldBeLogRequestAndResponse(HttpLoggingMiddleware sut, DefaultHttpContext context)
        {
            await sut.InvokeAsync(context);

            sut
                .Logger
                .Received(1)
                .Info("[Incoming Request] {Method} {Url}\n\n{Headers}\n\n{Body}",
                    Arg.Any<object[]>());
            sut
                .Logger
                .Received(1)
                .Info("[Incoming Response] {Method} {Url} {StatusCode} {StatusCodeDescription}\n\n{Headers}\n\n{Body}",
                    Arg.Any<object[]>());

        }
    }
}
