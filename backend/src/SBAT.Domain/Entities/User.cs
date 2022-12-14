using SBAT.Domain.Enums;
using SBAT.Domain.Interfaces;

namespace SBAT.Domain.Entities
{
    public class User : EntityBase
    {
        public string FirstNames { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public IdentityType IdentityType { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public int Age { get; private set; }
    }
}