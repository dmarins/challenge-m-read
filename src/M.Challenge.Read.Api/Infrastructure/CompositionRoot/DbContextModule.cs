using Autofac;
using M.Challenge.Read.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class DbContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var config = c.Resolve<IConfiguration>();

                    return new MongoClient(config.GetConnectionString("DefaultConnection"));
                })
                .As<IMongoClient>()
                .SingleInstance();

            builder
                .Register(c =>
                {
                    var config = c.Resolve<IConfiguration>();
                    var client = c.Resolve<IMongoClient>();

                    return client.GetDatabase(config.GetConnectionString("DatabaseName"));
                })
                .As<IMongoDatabase>()
                .SingleInstance();

            builder
                .RegisterType<DbContext>()
                .As<IDbContext>()
                .SingleInstance();
        }
    }
}
