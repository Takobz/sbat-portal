namespace SBAT.App.Models.SBATApi.Response
{
    public class SignInUserResponse
    {
        public string Username { get; set; } = string.Empty;
        public string JwtToken { get; set; } = string.Empty;
    }
}
