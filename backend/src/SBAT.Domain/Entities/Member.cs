using SBAT.Domain.Enums;
using SBAT.Domain.Interfaces;

namespace SBAT.Domain.Entities
{
    #pragma warning disable CS8618
    public class Member : EntityBase
    {
        public int UserId { get; private set; }
        public string PolicyNumber { get; private set; }
        public Relationship Relationship { get; private set; }

        public User User { get; private set; }
    }
}