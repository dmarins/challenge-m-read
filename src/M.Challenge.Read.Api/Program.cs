using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace M.Challenge.Read.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        private static string _environmentName;

        public static void Main(string[] args)
        {
            var webHost = BuildWebHost(args);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{_environmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom
                .Configuration(configuration)
                .CreateLogger();

            webHost.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .ConfigureLogging(
                    (hostingContext, config) =>
                    {
                        config.ClearProviders();
                        _environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                    })
                .UseStartup<Startup>()
                .Build();
        }
    }
}
