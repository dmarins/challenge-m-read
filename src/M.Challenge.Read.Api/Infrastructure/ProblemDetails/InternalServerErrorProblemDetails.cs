using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace M.Challenge.Read.Api.Infrastructure.ProblemDetails
{
    [ExcludeFromCodeCoverage]
    public class InternalServerErrorProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public InternalServerErrorProblemDetails(string details = null)
        {
            Title = "InternalServerError";
            Detail = details;
            Status = (int)HttpStatusCode.InternalServerError;
            Type = "https://httpstatuses.com/500";
        }
    }
}
