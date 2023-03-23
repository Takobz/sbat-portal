using SBAT.Core.Enums;

namespace SBAT.Core.Entities
{
    #pragma warning disable CS8618
    public class Policy : EntityBase
    {
        public string PrincipalMemberUserName { get; private set; }
        public string PolicyNumber { get; private set; }
        public IEnumerable<Member> Members { get; private set; }
        public PolicyType Type { get; private set; }

        public void AddMainMember(string userName)
        {
            PrincipalMemberUserName = userName;
        }

        public void AddPolicyNumber(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        public void AddMember(Member member)
        {
            if (Members is not null)
                Members = Members.Append(member); 

           Members = new List<Member>() { member };
        }
    }
}