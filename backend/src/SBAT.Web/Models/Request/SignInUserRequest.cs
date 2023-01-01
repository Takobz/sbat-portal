namespace SBAT.Web.Models.Request
{
    public class SignInUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}