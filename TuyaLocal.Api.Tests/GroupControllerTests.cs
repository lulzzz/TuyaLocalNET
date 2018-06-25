namespace TuyaLocal.Api.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class GroupControllerTests : TestBase
    {
        public GroupControllerTests(TestFixture<Startup> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GroupCreateShouldReturnOk()
        {
            var payload = new StringContent(
                @"{name: ""Wohnzimmer""}",
                Encoding.UTF8,
                "application/json");

            var result = await Client.PutAsync("/api/groups", payload);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GroupCreateShouldReturnBadRequest()
        {
            var payload = new StringContent(
                @"{name: """"}",
                Encoding.UTF8,
                "application/json");

            var result = await Client.PutAsync("/api/groups", payload);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GroupAddDeviceShouldReturnOK()
        {
            var payload = new StringContent(
                @"{name: ""Wohnzimmer""}",
                Encoding.UTF8,
                "application/json");

            var result = await Client.PutAsync("/api/groups", payload);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            payload = new StringContent(
                @"{id: ""1234678901""}",
                Encoding.UTF8,
                "application/json");

            result = await Client.PostAsync("/api/groups/Wohnzimmer", payload);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
