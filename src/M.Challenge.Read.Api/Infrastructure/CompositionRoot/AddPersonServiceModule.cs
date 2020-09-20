using Autofac;
using M.Challenge.Read.Application.Services.Person.Add;
using M.Challenge.Read.Domain.Services.Person;

namespace M.Challenge.Read.Api.Infrastructure.CompositionRoot
{
    public class AddPersonServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddPerson>().As<IAddPersonService>();
            builder.RegisterDecorator<AddPersonValidator, IAddPersonService>();
            builder.RegisterDecorator<AddPersonExceptionHandler, IAddPersonService>();
        }
    }
}
