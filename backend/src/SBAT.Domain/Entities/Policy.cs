using SBAT.Domain.Interfaces;

namespace SBAT.Domain.Entities
{
    #pragma warning disable CS8618
    public class Policy : EntityBase
    {
        public string PolicyNumber { get; private set; }
        public IEnumerable<Member> Members { get; private set; }
    }
}