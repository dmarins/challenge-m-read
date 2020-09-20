using M.Challenge.Read.Api.Infrastructure.Auth.Handlers;
using M.Challenge.Read.Api.Infrastructure.Auth.Policies;
using M.Challenge.Read.Api.Infrastructure.Auth.Requirements;
using M.Challenge.Read.Domain.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace M.Challenge.Read.Api
{
    public partial class Startup
    {
        public void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = ApiKeyConstants.DefaultScheme;
                    options.DefaultChallengeScheme = ApiKeyConstants.DefaultScheme;
                })
                .AddApiKeySupport(options => { });
        }

        public void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options
                    .AddPolicy(Policies.Writing,
                        policy => policy
                            .Requirements
                            .Add(new WritingRequirement()));
            });
        }
    }

    [ExcludeFromCodeCoverage]
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Scheme => ApiKeyConstants.DefaultScheme;
        public string AuthenticationType => ApiKeyConstants.DefaultScheme;
    }

    [ExcludeFromCodeCoverage]
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKeySupport(this AuthenticationBuilder authenticationBuilder, Action<ApiKeyAuthenticationOptions> options)
        {
            return authenticationBuilder
                .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyConstants.DefaultScheme, options);
        }
    }
}
