using SBAT.Core.Enums;
using SBAT.Core.Interfaces;

namespace SBAT.Core.Entities
{
#pragma warning disable CS8618
    public class Member : EntityBase
    {
        public string FirstNames { get; private set; }
        public string Surname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Relationship Relationship { get; private set; }

        public Policy Policy { get; private set; }
    }
}