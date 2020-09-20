using M.Challenge.Read.Domain.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M.Challenge.Read.Api.Infrastructure.Middlewares
{
    public class HttpLoggingMiddleware
    {
        public RequestDelegate Next { get; }
        public ILogger Logger { get; }

        public HttpLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context.Request);

            var originalBodyStream = context.Response.Body;

            await using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await Next(context);

                await LogResponse(context);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private string GetUrl(HttpRequest request)
        {
            var scheme = request.Scheme;
            var host = request.Host;
            var path = request.Path.HasValue ? request?.Path : null;

            return $"{scheme}://{host}{path}";
        }

        private string FormatHeaders(IHeaderDictionary headers)
        {
            if (headers.Count == 0) return string.Empty;

            var sb = new StringBuilder();

            foreach (var (key, value) in headers)
            {
                sb.Append($"{key}: {value}; ");
            }

            return sb.ToString();
        }

        private async Task LogRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            request.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            Logger.Info("[Incoming Request] {Method} {Url}\n\n{Headers}\n\n{Body}",
                request.Method.ToUpper(),
                GetUrl(request),
                FormatHeaders(request.Headers),
                Regex.Replace(bodyAsText, @"[\t\r\n'/\\]", ""));
        }

        private async Task LogResponse(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            response.Body.Seek(0, SeekOrigin.Begin);

            string body = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            var limit = body.Length < 500 ? body.Length : 500;

            Logger.Info("[Incoming Response] {Method} {Url} {StatusCode} {StatusCodeDescription}\n\n{Headers}\n\n{Body}",
                request.Method.ToUpper(),
                GetUrl(request),
                context.Response.StatusCode,
                ((HttpStatusCode)context.Response.StatusCode).ToString(),
                FormatHeaders(context.Request?.Headers),
                string.IsNullOrEmpty(body) ? null : body.Substring(0, limit));
        }
    }

    [ExcludeFromCodeCoverage]
    public static class HttpLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpLoggingMiddleware>();
        }
    }
}
