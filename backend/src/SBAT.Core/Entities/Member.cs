using SBAT.Core.Constants;
using SBAT.Core.Enums;
using SBAT.Core.Interfaces;

namespace SBAT.Core.Entities
{
#pragma warning disable CS8618
    public class Member : EntityBase
    {
        public string FirstNames { get; private set; }
        public string Surname { get; private set; }
        public int IdentityNumber { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string WorkTelephone { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public Relationship Relationship { get; private set; }
        public string StreetLine { get; set; } = string.Empty;
        public string SuburbOrTownLine { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = Countries.SA;

        public Policy Policy { get; private set; }
    }
}