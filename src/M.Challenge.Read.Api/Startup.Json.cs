using Microsoft.Extensions.DependencyInjection;

namespace M.Challenge.Read.Api
{
    public partial class Startup
    {
        private static void ConfigureJson(IServiceCollection services)
        {
            services
                .AddControllers(config => config.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
        }
    }
}
