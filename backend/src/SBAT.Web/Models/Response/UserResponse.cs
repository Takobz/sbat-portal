namespace SBAT.Web.Models.Response
{
    public class UserResponse : BaseDTO
    {
        public string FirstNames { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}