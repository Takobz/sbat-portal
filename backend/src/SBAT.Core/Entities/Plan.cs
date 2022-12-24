using System.ComponentModel.DataAnnotations.Schema;
using SBAT.Core.Enums;
using SBAT.Core.Interfaces;

namespace SBAT.Core.Entities
{
    #pragma warning disable CS8618
    public class Plan : EntityBase
    {
        public PlanType PlanType { get; private set; }
        public string PlanInformation { get; private set; }
        public double CashBack { get; private set; }
        public double PaymentAmount { get; private set; }
        public int WaitPeriod { get; private set; }

        [NotMapped]
        public IEnumerable<string> PlanServices { get; private set; }
    }
}