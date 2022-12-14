using SBAT.Domain.Enums;
using SBAT.Domain.Interfaces;

namespace SBAT.Domain.Entities
{
    public class Member : EntityBase
    {
        public int UserId { get; private set; }
        public string PolicyNumber { get; private set; } = string.Empty;
        public Relationship Relationship { get; private set; }

        public User User { get; private set; }
    }
}