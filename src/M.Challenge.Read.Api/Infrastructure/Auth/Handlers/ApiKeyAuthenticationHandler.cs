using M.Challenge.Read.Api.Infrastructure.ProblemDetails;
using M.Challenge.Read.Domain.Constants;
using M.Challenge.Read.Domain.Repositories.ApiKey;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace M.Challenge.Read.Api.Infrastructure.Auth.Handlers
{
    [ExcludeFromCodeCoverage]
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly JsonSerializerSettings JsonSerializerSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
            };

        public IInMemoryApiKeyRepository InMemoryApiKeyRepository { get; }

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IInMemoryApiKeyRepository inMemoryApiKeyRepository)
            : base(options,
                logger,
                encoder,
                clock)
        {
            InMemoryApiKeyRepository = inMemoryApiKeyRepository ?? throw new ArgumentNullException(nameof(inMemoryApiKeyRepository));
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyConstants.HeaderName, out var apiKeyHeaderValues))
            {
                return AuthenticateResult.NoResult();
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return AuthenticateResult.NoResult();
            }

            if (Guid.TryParse(providedApiKey, out var apiKeyParsed) == false)
            {
                return AuthenticateResult.NoResult();
            }

            var existingApiKey = await InMemoryApiKeyRepository.Execute(providedApiKey);
            if (existingApiKey == null)
            {
                return AuthenticateResult.Fail("Invalid api key provided.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingApiKey.Owner),
            };

            claims
                .AddRange(existingApiKey
                    .Roles
                    .Select(role =>
                        new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal(identities);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);

            return AuthenticateResult.Success(ticket);
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.ContentType = ProblemDetailsConstants.ContentType;

            var problemDetails = new UnauthorizedProblemDetails();

            await Response
                .WriteAsync(JsonConvert.SerializeObject(problemDetails,
                    Formatting.Indented,
                    JsonSerializerSettings));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.ContentType = ProblemDetailsConstants.ContentType;

            var problemDetails = new ForbiddenProblemDetails();

            await Response
                .WriteAsync(JsonConvert.SerializeObject(problemDetails,
                    Formatting.Indented,
                    JsonSerializerSettings));
        }
    }
}
