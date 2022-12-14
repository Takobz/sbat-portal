using SBAT.Domain.Enums;
using SBAT.Domain.Interfaces;

namespace SBAT.Domain.Entities
{
    public class Plan : EntityBase
    {
        public PlanType PlanType { get; private set; }
        public double CashBack { get; private set; }
        public IEnumerable<string> Services { get; private set; }
    }
}