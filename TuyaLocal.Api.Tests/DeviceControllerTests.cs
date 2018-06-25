namespace TuyaLocal.Api.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class DeviceControllerTests : TestBase
    {
        public DeviceControllerTests(TestFixture<Startup> fixture) : base(fixture)
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

        [Fact]
        public async Task GetDeviceList_ShouldReturnOK()
        {
            var getResult = await Client.GetAsync("/api/devices");

            Assert.Equal(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Fact]
        public async Task GetSingleDevice_ShouldReturn404()
        {
            const string id = "abdefg12345677";

            var getResult = await Client.GetAsync($"/api/devices/{id}");

            Assert.Equal(HttpStatusCode.NotFound, getResult.StatusCode);
        }

        [Fact]
        public async Task RemoveDevice_ShouldReturnBadRequest()
        {
            const string id = "abc4";

            var result = await Client.DeleteAsync($"/api/devices/{id}");

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task RemoveDevice_ShouldReturnOK()
        {
            const string id = "abdefg123491";

            var result = await Client.DeleteAsync($"/api/devices/{id}");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
