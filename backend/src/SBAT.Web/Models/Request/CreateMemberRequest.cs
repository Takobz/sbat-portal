using SBAT.Core.Constants;
using SBAT.Core.Enums;

namespace SBAT.Web.Models.Request
{
    public class CreateMemberRequest : BaseDTO
    {
        public string FirstNames { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; } = IdentityType.SAIdentification;
        public int IdentityNumber { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string WorkTelephone { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }
        public string StreetLine { get; set; } = string.Empty;
        public string SuburbOrTownLine { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = Countries.SA;
    }
}