namespace SBAT.Web.Models.Response
{
    public class SignInUserResponse
    {
        public string Username { get; set;} = string.Empty;
        public string JwtToken { get; set; } = string.Empty;
    }
}