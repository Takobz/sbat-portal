using SBAT.Domain.Interfaces;

namespace SBAT.Domain.Entities
{
    public class Policy : EntityBase
    {
        public string PolicyNumber { get; private set; } = string.Empty;
        public IEnumerable<Member> Members { get; private set; }
    }
}