using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace M.Challenge.Read.UnitTests.Config.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() =>
        {
            var fixture = new Fixture();

            fixture
                .Behaviors
                .Add(new OmitOnRecursionBehavior());

            fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture
                .Customize<BindingInfo>(c => c.OmitAutoProperties());

            return fixture.Customize(new AutoNSubstituteCustomization());
        })
        {
        }
    }
}
