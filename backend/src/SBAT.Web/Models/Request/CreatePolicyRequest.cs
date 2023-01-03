using SBAT.Core.Constants;
using SBAT.Core.Enums;

namespace SBAT.Web.Models.Request
{
    public class CreatePolicyRequest
    {
        public string MainMemberUserName { get; set; } = string.Empty;
        public MemberRequest MainMember { get; set; } = new MemberRequest();
    }

    public class MemberRequest 
    {
        public string FirstNames { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public IdentityType IdentityType { get; set; } = IdentityType.SAIdentification;
        public int IdentityNumber { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string WorkTelephone { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public Relationship Relationship { get; private set; } = Relationship.MainMember; //this should always be main on init create
        public string StreetLine { get; set; } = string.Empty;
        public string SuburbOrTownLine { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = Countries.SA;
    }
}