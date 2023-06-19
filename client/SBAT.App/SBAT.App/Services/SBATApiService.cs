using Microsoft.Extensions.Options;
using SBAT.App.Models.SBATApi;
using SBAT.App.Models.SBATApi.Constants;
using SBAT.App.Models.SBATApi.Request;
using SBAT.App.Models.SBATApi.Response;
using SBAT.App.Models.Settings;
using System.Text.Json;

namespace SBAT.App.Services
{
    public interface ISBATApiService
    {
        Task<SBATResponse<SignInUserResponse>> SignInUserAsync(SignInUserRequest signInUserRequest);
    }

    public class SBATApiService : ISBATApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public SBATApiService(IHttpClientFactory httpClientFactory, IOptions<SbatApiOptions> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(options.Value.BaseUri);
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = false };
        }

        public async Task<SBATResponse<SignInUserResponse>> SignInUserAsync(SignInUserRequest signInUserRequest)
        {
            var response = await _httpClient.GetAsync(SBATApiUrls.signInUserPath);
            //TODO: Propagate this exception to be handled by a common exception handler!
            return JsonSerializer.Deserialize<SBATResponse<SignInUserResponse>>(await response.Content.ReadAsStringAsync());
        }
    }
}
