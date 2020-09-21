using Microsoft.AspNet.OData.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace M.Challenge.Read.Api
{
    public partial class Startup
    {
        private static void ConfigureOData(IServiceCollection services)
        {
            services.AddOData();
        }
    }
}
