using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;
using SBAT.Web.Models.Response;

namespace SBAT.Integration.Tests.LoginControllerTests
{
    public class RegisterTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RegisterTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _factory.WithWebHostBuilder(builder => 
            {
                
            });
        }

        [Fact]
        public async Task Calling_POST_Register_With_Invalid_Data_Should_Return_BadRequest() 
        {
            var httpClient = _factory.CreateClient();
            var httpContent = new StringContent(string.Empty, new MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync("/api/login/register", httpContent);

            var apiResponse = await JsonSerializer.DeserializeAsync<Response<EmptyResponse>>(await response.Content.ReadAsStreamAsync());
            var x = await response.Content.ReadAsStringAsync();
            //Assert.
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Calling_POST_Register_With_Valid_Data_But_User_Exists_Should_Throw_BadRequest_With_Message() 
        {

        }
    }
}
