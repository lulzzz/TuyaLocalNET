namespace TuyaLocal.Api.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class DevicesUnitTest : TestBase
    {
        public DevicesUnitTest(TestFixture<Startup> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddDevice_ShouldReturnBadRequest()
        {
            var payload = new StringContent(
                @"{
                    id: ""12345678901"",
                    name: ""Fernseher""
                  }",
                Encoding.UTF8,
                "application/json");

            var result = await Client.PutAsync("/api/devices", payload);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task AddDevice_ShouldReturnOK()
        {
            var payload = new StringContent(
                @"{
                    id: ""12345678901"",
                    name: ""Fernseher"",
                    address: ""192.168.178.88"",
                    secretKey: ""asdfghjkl123""
                  }",
                Encoding.UTF8,
                "application/json");

            var result = await Client.PutAsync("/api/devices", payload);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
