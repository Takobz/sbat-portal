using SBAT.Core.Enums;
using SBAT.Core.Interfaces;

namespace SBAT.Core.Entities
{
    #pragma warning disable CS8618
    public class Policy : EntityBase
    {
        public string PrincipalMemberUserName { get; private set; }
        public string PolicyNumber { get; private set; }
        public IEnumerable<Member> Members { get; private set; }
        public PolicyType Type { get; private set; }

        public void CreateMainMember(string userName)
        {
            PrincipalMemberUserName = userName;
        }

        public void CreatePolicyNumber(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }
    }
}