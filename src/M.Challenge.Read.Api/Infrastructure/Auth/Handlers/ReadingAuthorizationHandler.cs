using M.Challenge.Read.Api.Infrastructure.Auth.Requirements;
using M.Challenge.Read.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace M.Challenge.Read.Api.Infrastructure.Auth.Handlers
{
    [ExcludeFromCodeCoverage]
    public class ReadingAuthorizationHandler : AuthorizationHandler<ReadingRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadingRequirement requirement)
        {
            if (context.User.IsInRole(Role.Reading))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
