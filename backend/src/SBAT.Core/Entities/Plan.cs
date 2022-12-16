using SBAT.Core.Enums;
using SBAT.Core.Interfaces;

namespace SBAT.Core.Entities
{
    #pragma warning disable CS8618
    public class Plan : EntityBase
    {
        public PlanType PlanType { get; private set; }
        public double CashBack { get; private set; }
        public IEnumerable<string> Services { get; private set; }
    }
}