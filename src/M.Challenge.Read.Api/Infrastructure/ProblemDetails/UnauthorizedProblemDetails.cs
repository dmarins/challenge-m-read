using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace M.Challenge.Read.Api.Infrastructure.ProblemDetails
{
    [ExcludeFromCodeCoverage]
    public class UnauthorizedProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public UnauthorizedProblemDetails(string details = null)
        {
            Title = "Unauthorized";
            Detail = details;
            Status = (int)HttpStatusCode.Unauthorized;
            Type = "https://httpstatuses.com/401";
        }
    }
}
