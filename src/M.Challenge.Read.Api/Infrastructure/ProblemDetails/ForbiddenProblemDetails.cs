using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace M.Challenge.Read.Api.Infrastructure.ProblemDetails
{
    [ExcludeFromCodeCoverage]
    public class ForbiddenProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public ForbiddenProblemDetails(string details = null)
        {
            Title = "Forbidden";
            Detail = details;
            Status = (int)HttpStatusCode.Forbidden;
            Type = "https://httpstatuses.com/403";
        }
    }
}
