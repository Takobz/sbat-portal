namespace SBAT.Web.Models.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstNames { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}