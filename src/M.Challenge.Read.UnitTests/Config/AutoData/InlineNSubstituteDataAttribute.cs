using AutoFixture.Xunit2;
using Xunit;

namespace M.Challenge.Read.UnitTests.Config.AutoData
{
    public class InlineNSubstituteDataAttribute : CompositeDataAttribute
    {
        public InlineNSubstituteDataAttribute(params object[] values)
            : base(new InlineDataAttribute(values), new AutoNSubstituteDataAttribute())
        {
        }
    }
}
