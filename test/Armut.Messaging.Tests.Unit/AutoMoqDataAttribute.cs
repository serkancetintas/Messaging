using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Armut.Messaging.Tests.Unit
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() =>
        {
            var fixture = new Fixture()
                 .Customize(new AutoMoqCustomization());

            return fixture;
        })
        {
        }
    }
}
