using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Read.Api.Infrastructure.Filters;
using M.Challenge.Read.Domain.Constants;
using M.Challenge.Read.Domain.Dtos;
using M.Challenge.Read.UnitTests.Config.AutoData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NSubstitute;
using System.Net;
using Xunit;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.Filters
{
    public class ModelStateValidatorFilterTests
    {
        [Theory, AutoNSubstituteData]
        public void Sut_ShouldHaveGuardClauses(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(ModelStateValidatorFilter).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Sut_WhenModelStateIsValid_ResultIsNull(ModelStateValidatorFilter sut, ActionExecutingContext context)
        {
            context.ModelState.Clear();
            context.Result = null;

            sut.OnActionExecuting(context);

            context.ModelState
                .IsValid
                .Should()
                .BeTrue();

            context
                .Result
                .Should()
                .BeNull();
        }

        [Theory, AutoNSubstituteData]
        public void Sut_WhenModelStateIsInvalid_ReturnsBadRequest(ModelStateValidatorFilter sut, ActionExecutingContext context)
        {
            const string error = ErrorMessageConstants.InvalidContract;

            var response = new ObjectResult(error)
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };

            sut.ResponseFactory
                .Return(Arg.Any<ResultDto>())
                .Returns(response);

            context.ModelState
                .AddModelError("error", "error");

            sut.OnActionExecuting(context);

            context.ModelState.IsValid
                .Should()
                .BeFalse();

            context.Result
                .As<ObjectResult>()
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.BadRequest);

            context.Result
                .As<ObjectResult>()
                .Value
                .Should()
                .Be(error);
        }
    }
}
