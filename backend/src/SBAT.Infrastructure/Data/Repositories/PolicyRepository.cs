using SBAT.Core.Entities;
using SBAT.Core.Enums;

namespace SBAT.Infrastructure.Data
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

        public IEnumerable<Policy> GetPolicies(PolicyType policyType)
        {
            return _iSbatDbContext.Policies.Where(p => p.Type == policyType)
                .AsEnumerable();
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
        IEnumerable<Policy> GetPolicies(PolicyType policyType);
    }
}