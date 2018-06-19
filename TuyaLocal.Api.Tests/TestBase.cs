namespace TuyaLocal.Api.Tests
{
    using System.Net.Http;
    using Xunit;

    public abstract class TestBase : IClassFixture<TestFixture<Startup>>
    {
        protected readonly HttpClient Client;

        protected TestBase(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }
    }
}
