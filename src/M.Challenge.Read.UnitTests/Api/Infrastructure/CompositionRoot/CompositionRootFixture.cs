using Autofac;
using M.Challenge.Read.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace M.Challenge.Read.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class CompositionRootFixture
    {
        public IContainer Container { get; }

        public CompositionRootFixture()
        {
            var configuration = Substitute.For<IConfigurationRoot>();

            configuration.GetConnectionString("DefaultConnection").Returns("mongodb+srv://dmarins:3vqxcE3FtUkuJdRm@cluster0.qvogf.gcp.mongodb.net/DemographicCensus?retryWrites=true&w=majority");
            configuration.GetConnectionString("DatabaseName").Returns("DemographicCensus");

            var services = new ServiceCollection();
            var startup = new Startup(configuration);

            startup.RegisterDependencies(services);

            Container = startup.Container;
        }
    }
}
