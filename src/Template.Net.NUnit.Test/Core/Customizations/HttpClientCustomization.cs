using AutoFixture;
using NSubstitute;
using Template.Net.NUnit.Test.Core.Http;

namespace Template.Net.NUnit.Test.Core.Customizations;

public sealed class HttpClientCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        //Register custom message handler
        fixture.Register<MockHttpMessageHandler>(() => Substitute.ForPartsOf<MockHttpMessageHandler>());
        //register http client
        fixture.Register(() =>
        {
            var handler = fixture.Create<MockHttpMessageHandler>();
            return new HttpClient(handler);
        });
    }
}