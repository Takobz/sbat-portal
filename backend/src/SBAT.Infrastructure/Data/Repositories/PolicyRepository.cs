using SBAT.Core.Entities;
using SBAT.Core.Enums;

namespace SBAT.Infrastructure.Data.Repos
{
    public class PolicyRepository : IPolicyRepository 
    {
        private readonly ISBATDbContext _iSbatDbContext;

        public PolicyRepository(ISBATDbContext iSbatDbContext)
        {
            _iSbatDbContext = iSbatDbContext ?? throw new ArgumentNullException(nameof(iSbatDbContext));
        }

        public Policy? CreatePolicy(Policy policy)
        {
            var newPolicyNumber = GeneratePolicyNumber(policy.Type);
            policy.AddPolicyNumber(newPolicyNumber);
            policy.AddMainMember(policy.PrincipalMemberUserName);
            _iSbatDbContext.Policies.Add(policy);
            _iSbatDbContext.Save();
            
            return _iSbatDbContext.Policies.FirstOrDefault(p => p.PolicyNumber == newPolicyNumber);
        }

        public IEnumerable<Policy> GetPolicies(string username)
        {
            return _iSbatDbContext.Policies.Where(p => p.PrincipalMemberUserName == username)
                .AsEnumerable();
        }

        public Policy? GetPolicy(string policyNumber)
        {
            return _iSbatDbContext.Policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
        }

        public void AddMember(string policyNumber, Member member)
        {
            var policy = _iSbatDbContext.Policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
            if (policy is not null)
                policy.AddMember(member);

            _iSbatDbContext.Save();
        }

        private string GeneratePolicyNumber(PolicyType policyType)
        {
            var numberOfPoliciesWithType = _iSbatDbContext.Policies
                .Where(x => x.Type == policyType).Count();

            return (policyType == PolicyType.Normal) ? $"ESF{numberOfPoliciesWithType}" 
                : $"ZAE{numberOfPoliciesWithType}";
        }
    }

    public interface IPolicyRepository
    {
        IEnumerable<Policy> GetPolicies(string username);
        Policy? CreatePolicy(Policy policy);
        Policy? GetPolicy(string policyNumber);
        void AddMember(string policyNumber, Member member);
    }
}