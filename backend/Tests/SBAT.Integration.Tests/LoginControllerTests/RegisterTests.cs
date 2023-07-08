using Microsoft.Net.Http.Headers;
using System.Net;

namespace SBAT.Integration.Tests.LoginControllerTests
{
    public class RegisterTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RegisterTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Calling_POST_Register_With_Invalid_Data_Should_Return_BadRequest() 
        {
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var httpContent = new StringContent(string.Empty);
            var response = await httpClient.PostAsync("/api/login/register", httpContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
