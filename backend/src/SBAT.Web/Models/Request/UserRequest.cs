using SBAT.Core.Enums;

namespace SBAT.Web.Models.Request
{
    public class UserRequest : BaseDTO
    {
        public string FirstNames { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public IdentityType Identity { get; set; }
        public int IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}