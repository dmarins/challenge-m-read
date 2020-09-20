using M.Challenge.Read.Api.Infrastructure.ProblemDetails;
using M.Challenge.Read.Domain.Constants;
using M.Challenge.Read.Domain.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace M.Challenge.Read.Api.Infrastructure.Middlewares
{
    public class UnexpectedErrorHandlingMiddleware
    {
        public RequestDelegate Next { get; }
        public ILogger Logger { get; }

        public UnexpectedErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                Logger.Error("An unexpected error occurred on the server.", ex);
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = ProblemDetailsConstants.ContentType;

            var result = JsonConvert
                .SerializeObject(
                    new InternalServerErrorProblemDetails
                    {
                        Detail = ex.Message
                    },
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new DefaultContractResolver()
                    });

            return context.Response.WriteAsync(result);
        }
    }

    [ExcludeFromCodeCoverage]
    public static class UnexpectedErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseUnexpectedErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnexpectedErrorHandlingMiddleware>();
        }
    }
}
