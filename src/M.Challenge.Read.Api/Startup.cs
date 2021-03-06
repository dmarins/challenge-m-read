using Autofac;
using M.Challenge.Read.Api.Infrastructure.Middlewares;
using M.Challenge.Read.Domain.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using System;
using System.Diagnostics.CodeAnalysis;

namespace M.Challenge.Read.Api
{
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ConfigureJson(services);
            ConfigureAuthentication(services);
            ConfigureAuthorization(services);
            ConfigureHealthChecks(services, Configuration);
            ConfigureOData(services);

            return RegisterDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseHealthChecks("/status");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpLoggingMiddleware();
            app.UseUnexpectedErrorHandlingMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Select().Filter().OrderBy().Count().Expand().MaxTop(10);
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());
            });
        }

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();

            odataBuilder.EntitySet<Person>(nameof(Person));

            return odataBuilder.GetEdmModel();
        }
    }
}
