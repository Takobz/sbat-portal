namespace SBAT.Web.Models.Response
{
    public class MemberResponse 
    {
        public string FirstNames { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public int IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; private set; }
    }
}